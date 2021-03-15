using ConsoleGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGenericRepository.DataAccessLayer
{
    public class UnitOfWork : IDisposable
    {
        private DoctorManagementDBEntities context = new DoctorManagementDBEntities();

        private GenericRepository<Doctor> doctorRepository;

        public GenericRepository<Doctor> DoctorRepository
        {
            get
            {

                if (this.doctorRepository == null)
                {
                    this.doctorRepository = new GenericRepository<Doctor>(context);
                }
                return doctorRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
