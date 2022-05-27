using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;  
using System.Data.SqlClient;
using System.Configuration;
namespace MvcApplication1.Models
{
    public class Studentcontext:DbContext   
    {
        public Studentcontext()
            : base("name=Studentcontext")
        {
        }
       
        //public DbSet<Student> student { get; set; }  
        public DbSet<Count> counts { get; set; }
        public DbSet<Script_Count> Script_counts { get; set; }
        public DbSet<Time_Table> TimeTables { get; set; }
        public DbSet<Current_session> CurrentsessionS { get; set; }
        public DbSet<Student_Master> Student_MasterS { get; set; }
        public DbSet<Exam_Marks_Actual> Exam_Marks_ActuaLS { get; set; }
        public DbSet<Student_Info> Student_InfoS { get; set; }
        public DbSet<Marks_Setup> Marks_SetupS { get; set; }
        public DbSet<Subject_Master> SubjectMasterS { get; set; }
        public DbSet<Sem_Marks> SemMarkS { get; set; }
        public DbSet<Student_Information> Student_InformationS { get; set; }
        public DbSet<PassedOutStudentsList> PassedOutStudentsListS { get; set; }
        public DbSet<Measurement_Evaluation> Measurement_EvaluationS { get; set; }
        
        }
   
   
    public class j2t
    {
        public string Dept { get; set; }
        public string  Sub_PCode { get; set; }
        public int Total { get; set; }
        public int Campus_ID { get; set; }
    }
}