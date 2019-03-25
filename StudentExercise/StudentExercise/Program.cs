using System;
using System.Collections.Generic;
using System.Linq;
using StudentExercise.Data;
using StudentExercise.Models;

namespace StudentExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();

            List<Exercise> exercises = repository.GetAllExercises();

            PrintExerciseReport("All Exercises", exercises);

            Pause();

            PrintExerciseByLanguage("All JS Exercises", "JavaScript", exercises);

            Pause();


            //Insert a new exercise into the database.

            Exercise gummies = new Exercise { Name = "gummies", Language = "JavaScript" };
            repository.AddExercise(gummies);

            exercises = repository.GetAllExercises();
            PrintExerciseReport("All exercises after adding gummies", exercises);

            Pause();

            List<Instructor> instructors = repository.GetAllInstructorsWithCohort();
            PrintAllInstructors("All Instructors with Cohort", instructors);

            Pause();

            Instructor Leah = new Instructor { FirstName = "Leah", LastName = "Hoef-something", SlackHandle = "@Leahhuh", CohortId = 2 };
            repository.AddInstructor(Leah);

            instructors = repository.GetAllInstructorsWithCohort();
            PrintAllInstructors("All Instructors After Adding Leah with Cohort", instructors);

            Pause();

            List<Student> students = repository.GetAllStudents();
            PrintAllStudents("All Students", students);

            Pause();

            repository.AddStudentExercise(2, 5);

            Pause();
        }


        //Query the database for all the Exercises.
        public static void PrintExerciseReport(string title, List<Exercise> exercises)
        {
            Console.WriteLine($"{title}");
            int i = 0;
            foreach(Exercise exercise in exercises)
            {
                i++;
                Console.WriteLine($"{i}. Name: {exercise.Name} Language: {exercise.Language}");
            }
        }


        //Find all the exercises in the database where the language is JavaScript.
        public static void PrintExerciseByLanguage(string title, string language, List<Exercise> exercises)
        {
            Console.WriteLine($"{title}");
            int i = 0;
            foreach(Exercise exercise in exercises)
            {
                i++;
                if(exercise.Language == $"{language}")
                {
                    Console.WriteLine($"{i}. Name: {exercise.Name} Language: {exercise.Language}");
                }
            }
        }


        //Find all instructors in the database.Include each instructor's cohort.
        public static void PrintAllInstructors(string title, List<Instructor> instructors)
        {
            Console.WriteLine($"{title}");
            foreach(Instructor instructor in instructors)
            {
                if(instructor.CohortId != null){
                    Console.WriteLine($"{instructor.FirstName} {instructor.LastName} Cohort: {instructor.Cohort.Name}");
                }
                else
                {
                    Console.WriteLine($"{instructor.FirstName} {instructor.LastName}");
                }
            }
        }

        public static void PrintAllStudents(string title, List<Student> students)
        {
            Console.WriteLine($"{title}");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
