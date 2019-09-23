using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL.SalesModel;

namespace ContosoUniversity.DAL.SalesModel.Mvc
{
	public class ItemsController : Controller
	{
		private IUnitOfWorkFactory uowFactory;
		private IRepository<Item> repository;

		public ItemsController()
		{
			SalesContext context = new SalesContext();
			this.uowFactory = new EntityFrameworkUnitOfWorkFactory(context);
			this.repository = new EntityFrameworkRepository<Item>(context);
		}
		
    public ItemsController(IUnitOfWorkFactory uowFactory, IRepository<Item> repository )
		{
			this.uowFactory = uowFactory;
			this.repository = repository;
		}

		//
		// GET: /Items

		public ViewResult Index(int? page, int? pageSize, string sortBy, bool? sortDesc )
		{
			// Defaults
			if (!page.HasValue)
				page = 1;
			if (!pageSize.HasValue)
				pageSize = 10;

			IQueryable<Item> query = repository.All();
			query = query.OrderBy(x => x.Id);
			
			// Paging
			int pageCount = (int)((query.Count() + pageSize - 1) / pageSize);
			if (page > 1)
				query = query.Skip((page.Value - 1) * pageSize.Value);
			query = query.Take(pageSize.Value);
			if (page > 1)
				ViewBag.Page = page.Value;
			if (pageSize != 10)
				ViewBag.PageSize = pageSize.Value;
			if (pageCount > 1) {
				int currentPage = page.Value;
				const int visiblePages = 5;
				const int pageDelta = 2;
				List<Tuple<string, bool, int>> paginationData = new List<Tuple<string, bool, int>>(); // text, enabled, page index
				paginationData.Add(new Tuple<string, bool, int>("Prev", currentPage > 1, currentPage - 1));
				if (pageCount <= visiblePages * 2) {
					for (int i = 1; i <= pageCount; i++)
						paginationData.Add(new Tuple<string, bool, int>(i.ToString(), true, i));
				}
				else {
					if (currentPage < visiblePages) {
						// 12345..10
						for (int i = 1; i <= visiblePages; i++)
							paginationData.Add(new Tuple<string, bool, int>(i.ToString(), true, i));
						paginationData.Add(new Tuple<string, bool, int>("...", false, -1));
						paginationData.Add(new Tuple<string, bool, int>(pageCount.ToString(), true, pageCount));
					}
					else if (currentPage > pageCount - (visiblePages - 1)) {
						// 1..678910
						paginationData.Add(new Tuple<string, bool, int>("1", true, 1));
						paginationData.Add(new Tuple<string, bool, int>("...", false, -1));
						for (int i = pageCount - (visiblePages - 1); i <= pageCount; i++)
							paginationData.Add(new Tuple<string, bool, int>(i.ToString(), true, i));
					}
					else {
						// 1..34567..10
						paginationData.Add(new Tuple<string, bool, int>("1", true, 1));
						paginationData.Add(new Tuple<string, bool, int>("...", false, -1));
						for (int i = currentPage - pageDelta, count = currentPage + pageDelta; i <= count; i++)
							paginationData.Add(new Tuple<string, bool, int>(i.ToString(), true, i));
						paginationData.Add(new Tuple<string, bool, int>("...", false, -1));
						paginationData.Add(new Tuple<string, bool, int>(pageCount.ToString(), true, pageCount));
					}
				}
				paginationData.Add(new Tuple<string, bool, int>("Next", currentPage < pageCount, currentPage + 1));
				ViewBag.PaginationData = paginationData;
			}

			// Sorting
			if (!string.IsNullOrEmpty(sortBy)) {
				bool ascending = !sortDesc.HasValue || !sortDesc.Value;
				if (sortBy == "ArDescription")
					query = OrderBy(query, x => x.ArDescription, ascending);
				if (sortBy == "EnDescription")
					query = OrderBy(query, x => x.EnDescription, ascending);
				if (sortBy == "Category")
					query = OrderBy(query, x => x.Category, ascending);
				if (sortBy == "RowVersion")
					query = OrderBy(query, x => x.RowVersion, ascending);
				if (sortBy == "Price")
					query = OrderBy(query, x => x.Price, ascending);
				ViewBag.SortBy = sortBy;
				if (sortDesc != null && sortDesc.Value)
					ViewBag.SortDesc = sortDesc.Value;
			}

			ViewBag.Entities = query.ToList();
			return View();
		}

		//
		// GET: /Items/Create

		public ActionResult Create()
		{
		    return View();
		} 
		
		//
		// POST: /Items/Create
		
		[HttpPost]
		public ActionResult Create(Item entity)
		{
			if (ModelState.IsValid)
				using (IUnitOfWork uow = uowFactory.Create()) {
					repository.Add(entity);
					uow.Save();
					return RedirectToAction("Index");
				}
			else
				return View();
		}

		//
		// GET: /Items/Details
		
		public ViewResult Details(long Id)
		{
			return View(repository.All().Single(x => x.Id == Id));
		}


		//
		// GET: /Items/Edit
				
		public ActionResult Edit(long Id)
		{
			var entity = repository.All().Single(x => x.Id == Id);
			return View(entity);
		}
				
		//
		// POST: /Items/Edit
				
		[HttpPost]
		public ActionResult Edit(Item entity)
		{
			if (ModelState.IsValid)
				using (IUnitOfWork uow = uowFactory.Create()) {
					Item original = repository.All().Single(x => x.Id == entity.Id);
					original.Id = entity.Id;
					original.ArDescription = entity.ArDescription;
					original.EnDescription = entity.EnDescription;
					original.Category = entity.Category;
					original.RowVersion = entity.RowVersion;
					original.Price = entity.Price;
					uow.Save();
					return RedirectToAction("Index");
				}
			else
				return View();
		}
		
		//
		// GET: /Items/Delete
		
		public ActionResult Delete(long Id)
		{
			return View(repository.All().Single(x => x.Id == Id));
		}
		
		//
		// POST: /Items/Delete
		
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(long Id)
		{
			using (IUnitOfWork uow = uowFactory.Create()) {
				repository.Remove(repository.All().Single(x => x.Id == Id));
				uow.Save();
				return RedirectToAction("Index");
			}
		}

		private static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, bool ascending) {

			return ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
		}
	}
}

