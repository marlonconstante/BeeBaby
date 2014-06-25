using System;
using Domain.Moment;
using Infrastructure.Repositories.Dropbox.Entities;
using Infrastructure.Repositories.Dropbox.Mapper;

namespace Infrastructure.Repositories.Dropbox
{
	public class DropboxMomentRepository: DropboxRepositoryBase<Moment, MomentData>, IMomentRepository
	{
		public DropboxMomentRepository(string datastoreId) : base(datastoreId, new DropboxMomentMapper())
		{
		}
	}
}

