using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonService.MVC.Models.Person;
using PesonService.DAL.Contract;
using PesonService.DAL.Entity;

namespace PersonService.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IRepository<PersonEntity> _repository;

        public PersonController(IRepository<PersonEntity> repository)
        {
            _repository = repository;
        }

        // GET: PersonEntities
        public async Task<IActionResult> Index()
        {
            return View(_repository.GetAll());
        }

        // GET: PersonEntities/Details/5
        public async Task<IActionResult> Details(Guid? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var personEntity = await _repository.GetAsync(id.Value, cancellationToken);

            if (personEntity == null)
            {
                return NotFound();
            }

            return View(personEntity);
        }

        // GET: PersonEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel personEntity, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _repository.InsertAsync(new PersonEntity
                {
                    Id = Guid.NewGuid(),
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName, 
                    BirthDate = personEntity.BirthDate,
                   
                }, cancellationToken);

                return RedirectToAction(nameof(Index));
            }
            return View(personEntity);
        }

        // GET: PersonEntities/Edit/5
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var personEntity = await _repository.GetAsync(id.Value, cancellationToken);

            if (personEntity == null)
            {
                return NotFound();
            }

            return View(personEntity);
        }

        // POST: PersonEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,BirthDate,Avatar,Resume,Id,CreatedAt,ModifiedAt")] PersonEntity personEntity, CancellationToken cancellationToken)
        {
            if (id != personEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(personEntity, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonEntityExists(personEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(personEntity);
        }

        // GET: PersonEntities/Delete/5
        public async Task<IActionResult> Delete(Guid? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var personEntity = await _repository.GetAsync(id.Value, cancellationToken);

            if (personEntity == null)
            {
                return NotFound();
            }

            return View(personEntity);
        }

        // POST: PersonEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
        {
            var personEntity = await _repository.GetAsync(id, cancellationToken);

            if (personEntity != null)
            {
                await _repository.DeleteAsync(id, cancellationToken);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonEntityExists(Guid id)
        {
            return _repository.GetAll().Any(e => e.Id == id);
        }
    }
}
