using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using StudentExercise.Models;

namespace StudentExercise.Data
{
    public class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                string connectionString = "Server = Localhost\\SQLEXPRESS; Database=DepartmentsAndEmployees;Trusted_Connection=True; Initial Catalog = StudentExercisesDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                return new SqlConnection(connectionString);
            }
        }


        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, Language FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);
                        int exerciseNamePosition = reader.GetOrdinal("Name");
                        string exerciseNameValue = reader.GetString(exerciseNamePosition);

                        int exerciseLanguagePosition = reader.GetOrdinal("Language");
                        string exerciseLanguageValue = reader.GetString(exerciseLanguagePosition);


                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = exerciseNameValue,
                            Language = exerciseLanguageValue
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        public List<Cohort> GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Cohort";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Cohort> cohorts = new List<Cohort>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);
                        int cohortNamePosition = reader.GetOrdinal("Name");
                        string cohortNameValue = reader.GetString(cohortNamePosition);


                        Cohort cohort= new Cohort
                        {
                            Id = idValue,
                            Name = cohortNameValue
                        };

                        cohorts.Add(cohort);
                    }

                    reader.Close();

                    return cohorts;
                }
            }
        }

        public void AddExercise(Exercise exercise)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Exercise (Name, Language) VALUES ('{exercise.Name}', '{exercise.Language}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Instructor (FirstName, LastName, SlackHandle, CohortId) VALUES('{instructor.FirstName}', '{instructor.LastName}', '{instructor.SlackHandle}', {instructor.CohortId})";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddStudentExercise(int StudentId, int ExerciseId)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO StudentExercise (StudentId, ExerciseId) VALUES ({StudentId}, {ExerciseId})";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT Id, FirstName, LastName, SlackHandle, CohortId
                        FROM Student";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
                            Exercises = new List<Exercise>()
                            
                        };
                        
                        students.Add(student);
                    }
                    reader.Close();

                    return students;

                }
            }
        }

        public List<Exercise> GetAllExerciseByStudentId(int studentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT e.id, e.Name, e.Language, t.StudentId
	                                    FROM StudentExercise t
	                                    RIGHT JOIN Exercise e on e.Id = t.ExerciseId
	                                    WHERE t.StudentId = {studentId}";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Language = reader.GetString(reader.GetOrdinal("Language"))
                        };
                        exercises.Add(exercise);
                    }
                    reader.Close();

                    return exercises;
                }
            }

        }

        public List<Instructor> GetAllInstructorsWithCohort()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT i.Id, i.FirstName, i.LastName, i.CohortId, c.Name
                        FROM Instructor i 
                        INNER JOIN Cohort c ON i.CohortId = c.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };
                        instructors.Add(instructor);
                    }
                    reader.Close();

                    return instructors;

                }
            }
        }
    }
}
