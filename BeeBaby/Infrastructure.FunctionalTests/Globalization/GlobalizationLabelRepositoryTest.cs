using NUnit.Framework;
using System;
using System.Linq;
using Infrastructure.Globalization;
using Skahal.Infrastructure.Framework.Globalization;

namespace Infrastructure.FunctionalTests.Globalization
{
	[TestFixture()]
	public class GlobalizationLabelRepositoryTest
	{
		[Test()]
		public void FindAll_DiffCultures_DiffResults()
		{
			var target = new GlobalizationLabelRepository();

			// pt-BR labels not loaded yet.
			var actual = target.FindAll(0, 5, f => f.CultureName.Equals("pt-BR")).ToList();
			Assert.AreEqual(0, actual.Count);

			Assert.IsTrue(target.LoadCultureLabels("es-ES"));
			actual = target.FindAll(0, 5, f => f.CultureName.Equals("pt-BR")).ToList();
			Assert.AreEqual(0, actual.Count);

			// pt-BR labels loaded.
			Assert.IsTrue(target.LoadCultureLabels("pt-BR"));
			actual = target.FindAll(0, 5, f => f.CultureName.Equals("pt-BR")).ToList();
			Assert.AreEqual(5, actual.Count);
			var label = actual[0];
			Assert.AreEqual("Baby", label.EnglishText);
			Assert.AreEqual("Bebe", label.CultureText);
			Assert.AreSame("pt-BR", label.CultureName);

			label = actual[1];
			Assert.AreEqual("Photo", label.EnglishText);
			Assert.AreEqual("Foto", label.CultureText);
			Assert.AreSame("pt-BR", label.CultureName);

			// es-ES labels loaded.
			//TODO: Usar multi indioma em tempo de execução
			Assert.IsFalse(target.LoadCultureLabels("es-ES"));
			actual = target.FindAll(0, 5, f => f.CultureName.Equals("es-ES")).ToList();
			Assert.AreEqual(2, actual.Count);
			label = actual[0];
			Assert.AreEqual("name", label.EnglishText);
			Assert.AreEqual("nombre", label.CultureText);
			Assert.AreSame("es-ES", label.CultureName);

			label = actual[1];
			Assert.AreEqual("first", label.EnglishText);
			Assert.AreEqual("primero", label.CultureText);
			Assert.AreSame("es-ES", label.CultureName);
		}
		[Test()]
		public void testssss()
		{
			GlobalizationService.Initialize(new GlobalizationLabelRepository());
			var actual = GlobalizationService.Translate("Baby");
			Assert.AreEqual("Bebe", actual);

			Assert.AreEqual("Bebe", "Baby".Translate());

		}
	}
}
