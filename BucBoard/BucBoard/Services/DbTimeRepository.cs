using BucBoard.Data;
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
	public class DbTimeRepository : ITimeRepository
	{

		private BucBoardDBContext _db;
		private AuthenticationDbContext _authenDb;
		private UserManager<ApplicationUser> _users;

		public DbTimeRepository(BucBoardDBContext db, AuthenticationDbContext authenDb, UserManager<ApplicationUser> users)
		{
			_db = db;
			_authenDb = authenDb;
			_users = users;
		}

		public Time CreateTime(Time time)
		{
			_db.Time.Add(time);
			_db.SaveChanges();
			return time;
		}

		public Time CreateTime(Time time, string id)
		{
			//_db.AspNetUsers.
			_db.Time.Add(time);
			_db.SaveChanges();
			return time;
		}

		public void DeleteTime(int id)
		{
			Time time = _db.Time.Find(id);
			_db.Time.Remove(time);
			_db.SaveChanges();
		}

		public ICollection<Time> ReadAllTime()
		{
			return _db.Time
				.Include(u => u.ApplicationUser)
				.Include(r => r.ApplicationUser.Roles)
				.ToList();
		}

		public Time ReadTime(int id)
		{
			return _db.Time.FirstOrDefault(a => a.Id == id);
		}

		public void UpdateTime(int id, Time time)
		{
			_db.Entry(time).State = EntityState.Modified;
			_db.SaveChanges();
		}

	}
}
