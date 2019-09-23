using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL.SalesModel;

namespace ContosoUniversity.DAL.SalesModel.Mvc
{
	public class ItemStoresController : Controller
	{
		private IUnitOfWorkFactory uowFactory;
		private IRepository<ItemStore> repository;
		private IRepository<Item> ItemRepository;
		private IRepository<Store> StoreRepository;

		public ItemStoresController()
		{
			SalesContext context = new SalesContext();
			this.uowFactory = new EntityFrameworkUnitOfWorkFactory(context);
			this.repository = new EntityFrameworkRepository<ItemStore>(context);
			this.ItemRepository = new EntityFrameworkRepository<Item>(context);
			this.StoreRepository = new EntityFrameworkRepository<Store>(context);
		}
		
    public ItemStoresController(IUnitOfWorkFactory uowFactory, IRepository<ItemStore> repository , IRepository<Item> ItemRepository, IRepository<Store> StoreRepository)
		{
			this.uowFactory = uowFactory;
			this.repository = repository;
			this.ItemRepository = ItemRepository;
			this.StoreRepository = StoreRepository;
		}

		//
		// GET: /ItemStores

		public ViewResult Index(int? page, int? pageSize, string sortBy, bool? sortDesc , long? ItemId, int? StoreId)
		{
			// Defaults
			if (!page.HasValue)
				page = 1;
			if (!pageSize.HasValue)
				pageSize = 10;

			IQueryable<ItemStore> query = repository.All();
			query = query.OrderBy(x => x.Id);
			// Filtering
			List<SelectListItem> selectList;
			if (ItemId != null) {
				query = query.Where(x => x.ItemId == ItemId);
				ViewBag.ItemId = ItemId;
			}
			selectList = new List<SelectListItem>();
			selectList.Add(new SelectListItem() { Text = null, Value = null, Selected = ItemId == null });
			selectList.AddRange(ItemRepository.All().ToList().Select(x => new SelectListItem() { Text = x.EnDescription.ToString(), Value = x.Id.ToString(), Selected = ItemId != null && ItemId == x.Id }));
			ViewBag.Items = selectList;
			ViewBag.SelectedItem = selectList.Where(x => x.Selected).Select(x => x.Text).FirstOrDefault();
			if (StoreId != null) {
				query = query.Where(x => x.StoreId == StoreId);
				ViewBag.StoreId = StoreId;
			}
			selectList = new List<SelectListItem>();
			selectList.Add(new SelectListItem() { Text = null, Value = null, Selected = StoreId == null });
			selectList.AddRange(StoreRepository.All().ToList().Select(x => new SelectListItem() { Text = x.Name.ToString(), Value = x.Id.ToString(), Selected = StoreId != null && StoreId == x.Id }));
			ViewBag.Stores = selectList;
			ViewBag.SelectedStore = selectList.Where(x => x.Selected).Select(x => x.Text).FirstOrDefault();
			
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
				if (sortBy == "Id")
					query = OrderBy(query, x => x.Id, ascending);
				if (sortBy == "MinLimit")
					query = OrderBy(query, x => x.MinLimit, ascending);
				if (sortBy == "MaxLimit")
					query = OrderBy(query, x => x.MaxLimit, ascending);
				if (sortBy == "Quantity")
					query = OrderBy(query, x => x.Quantity, ascending);
				if (sortBy == "ItemId")
					query = OrderBy(query, x => x.ItemId, ascending);
				if (sortBy == "StoreId")
					query = OrderBy(query, x => x.StoreId, ascending);
				ViewBag.SortBy = sortBy;
				if (sortDesc != null && sortDesc.Value)
					ViewBag.SortDesc = sortDesc.Value;
			}

			ViewBag.Entities = query.ToList();
			return View();
		}

		//
		// GET: /ItemStores/Create

		public ActionResult Create()
		{
			List<SelectListItem> selectList;
			selectList = new List<SelectListItem>();
			selectList.AddRange(ItemRepository.All().ToList().Select(x => new SelectListItem() { Text = x.EnDescription.ToString(), Value = x.Id.ToString(), Selected = null == x.Id }));
			ViewBag.Item = selectList;
			selectList = new List<SelectListItem>();
			selectList.AddRange(StoreRepository.All().ToList().Select(x => new SelectListItem() { Text = x.Name.ToString(), Value = x.Id.ToString(), Selected = null == x.Id }));
			ViewBag.Store = selectList;
		    return View();
		} 
		
		//
		// POST: /ItemStores/Create
		
		[HttpPost]
		public ActionResult Create(ItemStore entity)
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
		// GET: /ItemStores/Details
		
		public ViewResult Details(long Id)
		{
			return View(repository.All().Single(x => x.Id == Id));
		}


		//
		// GET: /ItemStores/Edit
				
		public ActionResult Edit(long Id)
		{
			var entity = repository.All().Single(x => x.Id == Id);
			List<SelectListItem> selectList;
			selectList = new List<SelectListItem>();
			selectList.AddRange(ItemRepository.All().ToList().Select(x => new SelectListItem() { Text = x.EnDescription.ToString(), Value = x.Id.ToString(), Selected = entity.ItemId == x.Id }));
			ViewBag.Item = selectList;
			selectList = new List<SelectListItem>();
			selectList.AddRange(StoreRepository.All().ToList().Select(x => new SelectListItem() { Text = x.Name.ToString(), Value = x.Id.ToString(), Selected = entity.StoreId == x.Id }));
			ViewBag.Store = selectList;
			return View(entity);
		}
				
		//
		// POST: /ItemStores/Edit
				
		[HttpPost]
		public ActionResult Edit(ItemStore entity)
		{
			if (ModelState.IsValid)
				using (IUnitOfWork uow = uowFactory.Create()) {
					ItemStore original = repository.All().Single(x => x.Id == entity.Id);
					original.Id = entity.Id;
					original.MinLimit = entity.MinLimit;
					original.MaxLimit = entity.MaxLimit;
					original.Quantity = entity.Quantity;
					original.ItemId = entity.ItemId;
					original.StoreId = entity.StoreId;
					uow.Save();
					return RedirectToAction("Index");
				}
			else
				return View();
		}
		
		//
		// GET: /ItemStores/Delete
		
		public ActionResult Delete(long Id)
		{
			return View(repository.All().Single(x => x.Id == Id));
		}
		
		//
		// POST: /ItemStores/Delete
		
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

