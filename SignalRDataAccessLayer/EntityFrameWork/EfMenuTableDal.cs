﻿using SignalRDataAccessLayer.Abstract;
using SignalRDataAccessLayer.Concrete;
using SignalRDataAccessLayer.Repositories;
using SignalREntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDataAccessLayer.EntityFrameWork
{
	public class EfMenuTableDal:GenericRepository<MenuTable>,IMenuTabledal

	{
		public EfMenuTableDal(SignalRContext context) : base(context) { }

		public int MenuTableCount()
		{
			using var context=new SignalRContext();
			return context.MenuTables.Count();
		}
	}
}
