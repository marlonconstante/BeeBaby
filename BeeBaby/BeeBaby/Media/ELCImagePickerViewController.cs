using System;
using UIKit;
using AssetsLibrary;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using System.Threading.Tasks;
using Skahal.Infrastructure.Framework.PCL.Globalization;
using BigTed;
using PixateFreestyleLib;
using System.Linq;

namespace ELCPicker
{
	public class AssetResult
	{
		public UIImage Image { get; set; }
	}

	public class ELCImagePickerViewController : UINavigationController
	{
		public int MaximumImagesCount { get; set; }

		readonly TaskCompletionSource<List<AssetResult>> _TaskCompletionSource = new TaskCompletionSource<List<AssetResult>>();

		public Task<List<AssetResult>> Completion {
			get {
				return _TaskCompletionSource.Task;
			}
		}

		public static ELCImagePickerViewController Instance {
			get {
				var albumPicker = new ELCAlbumPickerController();
				var picker = new ELCImagePickerViewController(albumPicker);
				albumPicker.Parent = picker;
				picker.MaximumImagesCount = 4;
				return picker;
			}
		}

		public void Dismiss()
		{
			DismissViewController(true, null);
		}

		ELCImagePickerViewController(UIViewController rootController) : base(rootController)
		{
			ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
		}

		void SelectedAssets(List<ALAsset> assets)
		{
			var results = new List<AssetResult>(assets.Count);
			foreach (var asset in assets)
			{
				var obj = asset.AssetType;
				if (obj == default (ALAssetType))
					continue;

				var rep = asset.DefaultRepresentation;
				if (rep != null)
				{
					var result = new AssetResult();
					result.Image = new UIImage(rep.GetImage(), rep.Scale, (UIImageOrientation) rep.Orientation);
					results.Add(result);
				}
			}

			_TaskCompletionSource.TrySetResult(results);
		}

		void CancelledPicker()
		{
			_TaskCompletionSource.TrySetCanceled();
		}

		bool ShouldSelectAsset(ALAsset asset, int previousCount)
		{
			var shouldSelect = MaximumImagesCount <= 0 || previousCount < MaximumImagesCount;
			if (!shouldSelect)
			{
				string message = string.Format("CannotImportPhotos".Translate(), MaximumImagesCount);
				var alert = new UIAlertView("Ops".Translate(), message, null, null, "GotIt".Translate());
				alert.Show();
			}
			return shouldSelect;
		}

		public class ELCAlbumPickerController : UITableViewController
		{
			static readonly NSObject _Dispatcher = new NSObject();
			readonly List<ALAssetsGroup> AssetGroups = new List<ALAssetsGroup>();

			ALAssetsLibrary Library;

			WeakReference _Parent;

			public ELCImagePickerViewController Parent {
				get {
					return _Parent == null ? null : _Parent.Target as ELCImagePickerViewController;
				}
				set {
					_Parent = new WeakReference(value);
				}
			}

			public ELCAlbumPickerController()
			{
			}

			public override void ViewDidLoad()
			{
				base.ViewDidLoad();

				NavigationController.View.AddStyleClass("navigation");
				NavigationItem.Title = "Albums".Translate();

				var cancelButton = new UIBarButtonItem(UIBarButtonSystemItem.Cancel);
				cancelButton.Clicked += CancelClicked;
				NavigationItem.RightBarButtonItem = cancelButton;

				TableView.Subviews[0].Alpha = 0f;
				TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

				AssetGroups.Clear();

				Library = new ALAssetsLibrary();
				Library.Enumerate(ALAssetsGroupType.All, GroupsEnumerator, GroupsEnumeratorFailed);
			}

			public override void ViewDidAppear(bool animated)
			{
				base.ViewDidAppear(animated);

				BTProgressHUD.Dismiss();
			}

			public override void ViewDidDisappear(bool animated)
			{
				base.ViewDidDisappear(animated);
				if (IsMovingFromParentViewController || IsBeingDismissed)
				{
					NavigationItem.RightBarButtonItem.Clicked -= CancelClicked;
				}
			}

			void CancelClicked(object sender = null, EventArgs e = null)
			{
				BTProgressHUD.Show();

				var parent = Parent;
				if (parent != null)
				{
					parent.CancelledPicker();
					parent.Dismiss();
				}
			}

			void GroupsEnumeratorFailed(Foundation.NSError error)
			{
				Console.WriteLine("Enumerator failed!");
			}

			void GroupsEnumerator(ALAssetsGroup agroup, ref bool stop)
			{
				if (agroup == null)
				{
					return;
				}

				// added fix for camera albums order
				if (agroup.Name.ToString().ToLower() == "camera roll" && agroup.Type == ALAssetsGroupType.SavedPhotos)
				{
					AssetGroups.Insert(0, agroup);
				}
				else
				{
					AssetGroups.Add(agroup);
				}

				_Dispatcher.BeginInvokeOnMainThread(ReloadTableView);
			}

			void ReloadTableView()
			{
				TableView.ReloadData();

				UIView.Animate(0.3d, () => {
					TableView.Subviews[0].Alpha = 1f;
				});
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return AssetGroups.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				const string cellIdentifier = "Cell";

				var cell = tableView.DequeueReusableCell(cellIdentifier);
				if (cell == null)
				{
					cell = new ELCAlbumCell(cellIdentifier);
				}

				// Get count
				var g = AssetGroups[indexPath.Row];
				g.SetAssetsFilter(ALAssetsFilter.AllPhotos);
				var gCount = g.Count.ToString();
				cell.TextLabel.Text = g.Name;
				cell.DetailTextLabel.Text = gCount;
				try
				{
					cell.ImageView.Image = new UIImage(g.PosterImage);
				}
				catch (Exception e)
				{
					Console.WriteLine("Failed to set thumbnail {0}", e);
				}
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var assetGroup = AssetGroups[indexPath.Row];
				assetGroup.SetAssetsFilter(ALAssetsFilter.AllPhotos);
				var picker = new ELCAssetTablePicker(assetGroup);
				picker.Parent = Parent;
				NavigationController.PushViewController(picker, true);
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return 88f;
			}

			class ELCAlbumCell : UITableViewCell
			{
				public ELCAlbumCell(string reuseIdentifier) : base(UITableViewCellStyle.Subtitle, reuseIdentifier)
				{
				}

				public override void LayoutSubviews()
				{
					base.LayoutSubviews();

					ImageView.Frame = new CGRect(5f, 5f, 78f, 78f);

					var textFrame = TextLabel.Frame;
					textFrame.X = 93f;
					textFrame.Width = Superview.Frame.Width - 123f;
					TextLabel.Frame = textFrame;

					var detailFrame = DetailTextLabel.Frame;
					detailFrame.X = 93f;
					detailFrame.Width = Superview.Frame.Width - 123f;
					DetailTextLabel.Frame = detailFrame;

					var indicator = Subviews.FirstOrDefault((view) => view is UIButton);
					if (indicator == null)
					{
						indicator = Subviews.FirstOrDefault((view) => view is UIScrollView).
									Subviews.FirstOrDefault((view) => view is UIButton);
					}

					var indicatorFrame = indicator.Frame;
					indicatorFrame.X = Superview.Frame.Width - 18f;
					indicator.Frame = indicatorFrame;
				}
			}
		}

		class ELCAssetTablePicker : UITableViewController
		{
			static readonly NSObject _Dispatcher = new NSObject();

			int Columns = 4;

			public bool SingleSelection { get; set; }

			public bool ImmediateReturn { get; set; }

			readonly ALAssetsGroup AssetGroup;

			readonly List<ELCAsset> ElcAssets = new List<ELCAsset>();

			WeakReference _Parent;

			public ELCImagePickerViewController Parent {
				get {
					return _Parent == null ? null : _Parent.Target as ELCImagePickerViewController;
				}
				set {
					_Parent = new WeakReference(value);
				}
			}

			public ELCAssetTablePicker(ALAssetsGroup assetGroup)
			{
				AssetGroup = assetGroup;
			}

			public override void ViewDidLoad()
			{
				TableView.Subviews[0].Alpha = 0f;
				TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
				TableView.AllowsSelection = false;
				TableView.ContentInset = new UIEdgeInsets(4f, 0f, 0f, 0f);

				NavigationItem.Title = AssetGroup.Name;

				if (!ImmediateReturn)
				{
					var doneButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done);
					doneButtonItem.Clicked += DoneClicked;
					NavigationItem.RightBarButtonItem = doneButtonItem;
				}

				Task.Run((Action) PreparePhotos);
			}

			public override void ViewWillAppear(bool animated)
			{
				base.ViewWillAppear(animated);
				Columns = (int) (View.Bounds.Size.Width / 80f);
			}

			public override void ViewDidDisappear(bool animated)
			{
				base.ViewDidDisappear(animated);
				if (IsMovingFromParentViewController || IsBeingDismissed)
				{
					NavigationItem.RightBarButtonItem.Clicked -= DoneClicked;
				}
			}

			public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
			{
				base.WillRotate(toInterfaceOrientation, duration);

				UIView.Animate(duration, () => {
					TableView.Subviews[0].Alpha = 0f;
				});
			}

			public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
			{
				base.DidRotate(fromInterfaceOrientation);

				Columns = (int) (View.Bounds.Size.Width / 80f);
				ReloadAndScrollToBottom();
			}

			void PreparePhotos()
			{
				AssetGroup.Enumerate(PhotoEnumerator);

				_Dispatcher.BeginInvokeOnMainThread(() => {
					ReloadAndScrollToBottom();
				});
			}

			void ReloadAndScrollToBottom()
			{
				TableView.ReloadData();
				int section = NumberOfSections(TableView) - 1;
				int row = TableView.NumberOfRowsInSection(section) - 1;
				if (section >= 0 && row >= 0)
				{
					var ip = NSIndexPath.FromRowSection(row, section);
					TableView.ScrollToRow(ip, UITableViewScrollPosition.Bottom, false);

					UIView.Animate(0.3d, () => {
						TableView.Subviews[0].Alpha = 1f;
					});
				}
			}

			void PhotoEnumerator(ALAsset result, int index, ref bool stop)
			{
				if (result == null)
				{
					return;
				}

				ELCAsset elcAsset = new ELCAsset(this, result);

				bool isAssetFiltered = false;

				if (result.DefaultRepresentation == null)
					isAssetFiltered = true;

				if (!isAssetFiltered)
				{
					ElcAssets.Add(elcAsset);
				}
			}

			void DoneClicked(object sender = null, EventArgs e = null)
			{
				BTProgressHUD.Show();

				var selected = new List<ALAsset>();

				foreach (var asset in ElcAssets)
				{
					if (asset.Selected)
					{
						selected.Add(asset.Asset);
					}
				}

				var parent = Parent;
				if (parent != null)
				{
					parent.SelectedAssets(selected);
					parent.Dismiss();
				}
			}

			bool ShouldSelectAsset(ELCAsset asset)
			{
				int selectionCount = TotalSelectedAssets;
				bool shouldSelect = true;

				var parent = Parent;
				if (parent != null)
				{
					shouldSelect = parent.ShouldSelectAsset(asset.Asset, selectionCount);
				}

				return shouldSelect;
			}

			void AssetSelected(ELCAsset asset, bool selected)
			{
				TotalSelectedAssets += (selected) ? 1 : -1;

				if (SingleSelection)
				{
					foreach (var elcAsset in ElcAssets)
					{
						if (asset != elcAsset)
						{
							elcAsset.Selected = false;
						}
					}
				}
				if (ImmediateReturn)
				{
					var parent = Parent;
					var obj = new List<ALAsset>(1);
					obj.Add(asset.Asset);
					parent.SelectedAssets(obj);
				}
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableView, nint section)
			{
				if (Columns <= 0)
					return 4;
				int numRows = (int) Math.Ceiling((nfloat) ElcAssets.Count / Columns);
				return numRows;
			}

			List<ELCAsset> AssetsForIndexPath(NSIndexPath path)
			{
				int index = path.Row * Columns;
				int length = Math.Min(Columns, ElcAssets.Count - index);
				return ElcAssets.GetRange(index, length);
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				const string cellIdentifier = "Cell";

				var cell = TableView.DequeueReusableCell(cellIdentifier) as ELCAssetCell;
				if (cell == null)
				{
					cell = new ELCAssetCell(UITableViewCellStyle.Default, cellIdentifier);
				}
				cell.SetAssets(AssetsForIndexPath(indexPath), Columns);
				return cell;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return 79f;
			}

			public int TotalSelectedAssets;

			public class ELCAsset
			{
				public readonly ALAsset Asset;
				readonly WeakReference _Parent;

				bool _Selected;

				public ELCAsset(ELCAssetTablePicker parent, ALAsset asset)
				{
					_Parent = new WeakReference(parent);
					Asset = asset;
				}

				public void ToggleSelected()
				{
					Selected = !Selected;
				}

				public bool Selected { 
					get { 
						return _Selected;
					}

					set {
						var parent = _Parent.Target as ELCAssetTablePicker;
						if (value && parent != null && !parent.ShouldSelectAsset(this))
						{
							return;
						}

						_Selected = value;

						if (parent != null)
						{
							parent.AssetSelected(this, value);
						}
					}
				}
			}

			class ELCAssetCell : UITableViewCell
			{
				List<ELCAsset> RowAssets;
				int Columns;
				readonly List<UIImageView> ImageViewArray = new List<UIImageView>();
				readonly List<UIView> OverlayViewArray = new List<UIView>();

				public ELCAssetCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
				{
					UITapGestureRecognizer tapRecognizer = new UITapGestureRecognizer(CellTapped);
					AddGestureRecognizer(tapRecognizer);
				}

				public void SetAssets(List<ELCAsset> assets, int columns)
				{
					RowAssets = assets;
					Columns = columns;

					foreach (var view in ImageViewArray)
					{
						view.RemoveFromSuperview();
					}
					foreach (var view in OverlayViewArray)
					{
						view.RemoveFromSuperview();
					}

					while (ImageViewArray.Count > RowAssets.Count)
					{
						ImageViewArray.RemoveAt(ImageViewArray.Count - 1);
					}

					while (OverlayViewArray.Count > RowAssets.Count)
					{
						OverlayViewArray.RemoveAt(OverlayViewArray.Count - 1);
					}

					for (int i = 0; i < RowAssets.Count; i++)
					{
						var asset = RowAssets[i];

						if (i < ImageViewArray.Count)
						{
							var imageView = ImageViewArray[i];
							imageView.Image = new UIImage(asset.Asset.Thumbnail);
						}
						else
						{
							var imageView = new UIImageView(new UIImage(asset.Asset.Thumbnail));
							ImageViewArray.Add(imageView);
						}

						if (i < OverlayViewArray.Count)
						{
							var overlayView = OverlayViewArray[i];
							overlayView.Hidden = !asset.Selected;
						}
						else
						{
							var overlayView = new UIView(new CGRect(0f, 0f, 75f, 75f));
							overlayView.SetStyleClass("selected-cell");

							var checkmarkView = new UIView(new CGRect(50f, 50f, 20f, 20f));
							checkmarkView.SetStyleClass("checkmark");

							overlayView.AddSubview(checkmarkView);

							OverlayViewArray.Add(overlayView);
							overlayView.Hidden = !asset.Selected;
						}
					}
				}

				void CellTapped(UITapGestureRecognizer tapRecognizer)
				{
					PointF point = tapRecognizer.LocationInView(this);
					var totalWidth = Columns * 75 + (Columns - 1) * 4;
					var startX = (Bounds.Size.Width - totalWidth) / 2;

					var frame = new CGRect(startX, 2, 75, 75);
					for (int i = 0; i < RowAssets.Count; ++i)
					{
						if (frame.Contains(point))
						{
							ELCAsset asset = RowAssets[i];
							asset.Selected = !asset.Selected;
							var overlayView = OverlayViewArray[i];
							overlayView.Hidden = !asset.Selected;
							break;
						}
						var x = frame.X + frame.Width + 4;
						frame = new CGRect(x, frame.Y, frame.Width, frame.Height);
					}
				}

				public override void LayoutSubviews()
				{
					var totalWidth = Columns * 75 + (Columns - 1) * 4;
					var startX = (Bounds.Size.Width - totalWidth) / 2;

					var frame = new CGRect(startX, 0, 75, 75);

					int i = 0;
					foreach (var imageView in ImageViewArray)
					{
						imageView.Frame = frame;
						AddSubview(imageView);

						var overlayView = OverlayViewArray[i++];
						overlayView.Frame = frame;
						AddSubview(overlayView);

						var x = frame.X + frame.Width + 4;
						frame = new CGRect(x, frame.Y, frame.Width, frame.Height);
					}
				}
			}
		}
	}
}