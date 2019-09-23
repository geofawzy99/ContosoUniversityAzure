using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL.SalesModel;
using ContosoUniversity.DAL.SalesModel.Repository;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace ContosoUniversity.DAL.SalesModel.Mvc
{
	public class BooksController : Controller
	{
		private IUnitOfWorkFactory uowFactory;
		private BookRepository repository;
		private PublisherRepository PublisherRepository;

		public BooksController()
		{
			SalesContext context = new SalesContext();
			this.uowFactory = new EntityFrameworkUnitOfWorkFactory(context);
			this.repository = new BookRepository(context);
			this.PublisherRepository = new PublisherRepository(context);
		}
		
    public BooksController(IUnitOfWorkFactory uowFactory, BookRepository repository , PublisherRepository PublisherRepository)
		{
			this.uowFactory = uowFactory;
			this.repository = repository;
			this.PublisherRepository = PublisherRepository;
		}

		//
		// GET: /Books

		public ViewResult Index(int? page, int? pageSize, string sortBy, bool? sortDesc , int? PublisherId)
		{
			// Defaults
			if (!page.HasValue)
				page = 1;
			if (!pageSize.HasValue)
				pageSize = 10;

			IQueryable<Book> query = repository.GetAll().AsQueryable();
			// Filtering
			List<SelectListItem> selectList;
			if (PublisherId != null) {
				query = query.Where(x => x.PublisherId == PublisherId);
				ViewBag.PublisherId = PublisherId;
			}
			selectList = new List<SelectListItem>();
			selectList.Add(new SelectListItem() { Text = null, Value = null, Selected = PublisherId == null });
			selectList.AddRange(PublisherRepository.GetAll().ToList().Select(x => new SelectListItem() { Text = x.Name.ToString(), Value = x.Id.ToString(), Selected = PublisherId != null && PublisherId == x.Id }));
			ViewBag.Publishers = selectList;
			ViewBag.SelectedPublisher = selectList.Where(x => x.Selected).Select(x => x.Text).FirstOrDefault();
			
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
				if (sortBy == "ISBN")
					query = OrderBy(query, x => x.ISBN, ascending);
				if (sortBy == "Publisher")
					query = OrderBy(query, x => x.PublisherId, ascending);
				if (sortBy == "Langauge")
					query = OrderBy(query, x => x.Langauge, ascending);
                if (sortBy == "ArDescription")
                    query = OrderBy(query, x => x.ArDescription, ascending);
                if (sortBy == "EnDescription")
                    query = OrderBy(query, x => x.EnDescription, ascending);
                if (sortBy == "Category")
                    query = OrderBy(query, x => x.Category, ascending);
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
		// GET: /Books/Create

		public ActionResult Create()
		{
            PopulatePublisherDropDownList();
            return View();
		} 
		
		//
		// POST: /Books/Create
		
		[HttpPost]
		public ActionResult Create(Book entity)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    using (IUnitOfWork uow = uowFactory.Create())
                    {
                        repository.Add(entity);
                        uow.Save();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulatePublisherDropDownList(entity.PublisherId);
            return View(entity);
		}

        // GET: /Items/Details

        public ActionResult Details(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = repository.GetAll().Single(x => x.Id == Id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }


        //
        // GET: /Items/Edit

        public ActionResult Edit(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = repository.GetAll().Single(x => x.Id == Id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            PopulatePublisherDropDownList(entity.PublisherId);
            return View(entity);
        }

        //
        // POST: /Items/Edit

        [HttpPost]
        public ActionResult Edit(Book entity)
        {
            try
            {
                if (ModelState.IsValid)
                using (IUnitOfWork uow = uowFactory.Create())
                {
                    Book original = repository.GetAll().Single(x => x.Id == entity.Id);
                    original.Id = entity.Id;
                    original.ArDescription = entity.ArDescription;
                    original.EnDescription = entity.EnDescription;
                    original.Category = entity.Category;
                    original.Price = entity.Price;
                    original.PublisherId = entity.PublisherId;
                    original.Langauge = entity.Langauge;
                    original.ISBN = entity.ISBN;
                    uow.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulatePublisherDropDownList(entity.PublisherId);
            return View(entity);
        }

        //
        // GET: /Items/Delete

        public ActionResult Delete(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = repository.GetAll().Single(x => x.Id == Id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        //
        // POST: /Items/Delete

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long Id)
        {
            using (IUnitOfWork uow = uowFactory.Create())
            {
                repository.Remove(repository.GetAll().Single(x => x.Id == Id));
                uow.Save();
                return RedirectToAction("Index");
            }
        }


        private static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(IQueryable<TSource> source, System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector, bool ascending) {

			return ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
		}

        private void PopulatePublisherDropDownList(int? selectedPublisher = null)
        {
            List<SelectListItem> selectList;
            selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem() { Text = null, Value = null, Selected = selectedPublisher == null });
            selectList.AddRange(PublisherRepository.GetAll().ToList().Select(x => new SelectListItem() { Text = x.Name.ToString(), Value = x.Id.ToString(), Selected = selectedPublisher != null && selectedPublisher == x.Id }));
            ViewBag.Publisher = selectList;
        }
    }
}

