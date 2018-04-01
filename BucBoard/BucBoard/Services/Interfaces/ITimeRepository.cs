using BucBoard.Models.Entities.Existing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
	public interface ITimeRepository
	{
		Time CreateTime(Time time);
		Time ReadTime(int id);
		ICollection<Time> ReadAllTime();
		void UpdateTime(int id, Time time);
		void DeleteTime(int id);
	}
}


