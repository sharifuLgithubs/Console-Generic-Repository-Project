using ConsoleGenericRepository.DataAccessLayer;
using ConsoleGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGenericRepository
{
    class Program
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        static void Main(string[] args)
        {
            bool programRun = true;
            while (programRun)
            {
                Console.WriteLine("Doctors Information>> Press 'L' to view doctor list, 'A' to add, 'E' to edit, 'D' to delete, 'C' to clean, 'X' to exit");
                string command = Console.ReadLine();
                if (command == "L")
                {
                    Index();
                }
                else if (command == "A")
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();

                    Console.Write("Designation: ");
                    var designation = Console.ReadLine();

                    var doctor = new Doctor();
                    doctor.DoctorName = name;
                    doctor.Designation = designation;

                    Create(doctor);
                }
                else if (command == "E")
                {
                    Console.Write("Id: ");
                    var id = Console.ReadLine();
                    var doctor = Details(Convert.ToInt32(id));
                    if (doctor != null)
                    {
                        Console.WriteLine("Id: " + doctor.DoctorId + "  Name: " + doctor.DoctorName + "   Designation: " + doctor.Designation);

                        Console.Write("Name: ");
                        var name = Console.ReadLine();

                        Console.Write("Designation: ");
                        var designation = Console.ReadLine();

                        doctor.DoctorName = name;
                        doctor.Designation = designation;

                        Edit(doctor);
                    }
                }
                else if (command == "D")
                {
                    Console.Write("Id: ");
                    var id = Console.ReadLine();
                    var doctor = Details(Convert.ToInt32(id));
                    if (doctor != null)
                    {
                        Console.WriteLine("Id: " + doctor.DoctorId + "  Name: " + doctor.DoctorName + "   Designation: " + doctor.Designation);
                        Console.WriteLine("Are you sure to delete, press 'Y' to continue, 'N' to back");
                        string confirm = Console.ReadLine();
                        if (confirm == "Y")
                        {
                            DeleteConfirmed(doctor.DoctorId);
                        }
                        else if (confirm == "N")
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (command == "X")
                {
                    programRun = false;
                }
                else if (command == "C")
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Wrong command");
                }
            }
        }

        // GET: Doctor
        public static void Index()
        {
            var doctors = unitOfWork.DoctorRepository.Get(includeProperties: "");
            foreach (var doctor in doctors.ToList())
            {
                Console.WriteLine("Id: " + doctor.DoctorId + "  Name: " + doctor.DoctorName + "   Designation: " + doctor.Designation);
            }
        }

        public static void Create(Doctor doctor)
        {
            try
            {
                unitOfWork.DoctorRepository.Insert(doctor);
                unitOfWork.Save();
                Console.WriteLine("New Doctor added successfully.");
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex);
            }
        }

        public static Doctor Details(int id)
        {
            Doctor doctor = unitOfWork.DoctorRepository.GetByID(id);
            return doctor;
        }

        public static void Edit(Doctor doctor)
        {
            try
            {
                unitOfWork.DoctorRepository.Update(doctor);
                unitOfWork.Save();
                Console.WriteLine("Doctor updated successfully.");
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex);
            }
        }

        public static void DeleteConfirmed(int id)
        {
            Doctor doctor = unitOfWork.DoctorRepository.GetByID(id);
            unitOfWork.DoctorRepository.Delete(id);
            unitOfWork.Save();
            Console.WriteLine("Doctor deleted successfully.");
        }
    }
}
