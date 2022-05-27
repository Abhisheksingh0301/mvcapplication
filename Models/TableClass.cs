using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;
namespace MvcApplication1.Models
{
/*----------------------------TIME_TABLE-----------------------------------------*/
    [Table("Time_Table")]
    public class Time_Table
    {

        public string Department { get; set; }
        public string Semester { get; set; }
        [Key]
        [Column(Order = 1)]
        public String Subject { get; set; }
        public string SubTitle { get; set; }
        [Key]
        [Column(Order = 2)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="{0:dd-MMM-yy}")]
        public DateTime DOE { get; set; }
        [Key]
        [Column(Order = 3)]
        public DateTime Time_From { get; set; }
        public DateTime Time_To { get; set; }
        public string Duration { get; set; }
        public bool Practical { get; set; }
        [Key]
        [Column(Order = 0)]
        public string Sessn { get; set; }

    }
/*-------------------------------------STUDENT_MASTER----------------------------*/
[Table("STUDENT_MASTER")]
public class Student_Master
{
	[Key]
	[Column(Order = 0)]
    public String Student_id{ get; set; }
	public String Sess{ get; set; }
	public int Current_Year{ get; set; }
	public String Name{ get; set; }
	public String Gradian_Name{ get; set; }
	public String Sex{ get; set; }
	public String Department{ get; set; }
	public String Class{ get; set; }
    [Required(ErrorMessage="Roll number cannot be blank")]
	public String Roll{ get; set; }
	[Key]
	[Column(Order = 1)]
	public String RegistrationNo{ get; set; }
	public String Specialisation{ get; set; }
	public String Photo{ get; set; }
	public bool Repeater{ get; set; }
	public bool ExtraCrdtCourse{ get; set; }
	public int Campus_Id{ get; set; }
	public int GP_No{ get; set; }
}

/*-----------------SUBJECT_MASTER-------------------------------------------*/
[Table("SUBJECT_MASTER")]
public class Subject_Master
{
    [Key]
    public String Subcode { get; set; }
    public String SubTitle { get; set; }
    public String SubType { get; set; }
    public String Old_Code { get; set; }
    public String Type { get; set; }
    public String Level { get; set; }
    public String Semester { get; set; }
    public String Course { get; set; }
    public String Dept { get; set; }
}
/*          EXAM_MARKS_ACTUAL-----------------------------------------------*/
[Table("EXAM_MARKS_ACTUAL")]
public class Exam_Marks_Actual
{
	[Key]
	[Column(Order = 0)]
    public String Student_id{ get; set; }
	[Key]   
	[Column(Order = 1)]
        public String RegNo{ get; set; }
	[Key]
	[Column(Order = 2)]
	public int Current_Year{ get; set; }
	[Key]
	[Column(Order = 3)]
	public String Roll{ get; set; }
	[Key]
	[Column(Order = 4)]
	public String Department{ get; set; }
	[Key]
	[Column(Order = 5)]
	public String Class{ get; set; }
	[Key]
	[Column(Order = 6)]
	public String Subject{ get; set; }
	[Key]
	[Column(Order = 7)]
	public String Sess{ get; set; }
	[Key]
	[Column(Order = 8)]
	public String SemCode{ get; set; }
	public double CA{get; set;}
	public string CA_AP {get; set;}
	public double INT {get; set;}
	public string INT_AP {get; set;}
	public double EXT{get; set;}
	public string EXT_AP {get; set;}
	public double AVERAGE {get; set;}
	public double WEIGHTAGE {get; set;}
	public double PLUS {get; set;}
	public double TOTAL {get; set;}
	public string PASS {get; set;}
	public string CREDIT {get; set;}
	public string Remarks {get; set;}
	public double Crdt {get; set;}
	public int GrdPoint {get; set;}
	
}
/* Current Session Table--------------------------------------------------------------*/
[Table("CURRENT_SESSION")]
public class Current_session
{
	[Key]
    [Required]
    [Column("Current_session")]
    [Display(Name = "Current_session")]
	public string Current_sess {get; set;}
}
/*****************MARKS_SETUP********************************************************/
[Table("MARKS_SETUP")]
public class Marks_Setup
{
    [Key]
    [Column(Order = 0)]
    public String Sess { get; set; }
    [Key]
    [Column(Order = 1)]
    public int Current_Year { get; set; }
    [Key]
    [Column(Order = 2)]
    public String Semester { get; set; }
    public String Department { get; set; }
    [Key]
    [Column(Order = 3)]
    public String Subject_Code { get; set; }
    public int FullMarks { get; set; }
    public int CA { get; set; }
    public int EA { get; set; }
    public int WTG { get; set; }
    public int PassMarks { get; set; }
    public int Grace { get; set; }
    public int Credit { get; set; }
    public int BorderUp { get; set; }
    public int BorderLow { get; set; }
}
/*************************SEM_MARKS TABLE*******************************************/
[Table("Sem_Marks")]
public class Sem_Marks
{
    public string Ori_Roll_No { get; set; }
    public string Cod_Roll_No { get; set; }
    [Key]
    [Column (Order=1)]
    public string Sub_PCode { get; set; }
    public string Ori_SCode { get; set; }
    public string Sem_No { get; set; }
    public string Dept { get; set; }
    [Column("Session")]
    [Display(Name = "Session")]
    public string Sess { get; set; }
    public float I_Gr_A_Mrks { get; set; }
    public float I_Gr_B_Mrks { get; set; }
    public float I_Gr_C_Mrks { get; set; }
    public float I_Gr_D_Mrks { get; set; }
    public float E_Gr_A_Mrks { get; set; }
    public float E_Gr_B_Mrks { get; set; }
    public float E_Gr_C_Mrks { get; set; }
    public float E_Gr_D_Mrks { get; set; }
    public float Tot_Mrks { get; set; }
    public int Pkt_No { get; set; }
    [Key]
    [Column (Order=0)]
    public string Regn_No { get; set; }
    public string Com_Dept { get; set; }
    public float FE_Gr_A_Mrks { get; set; }
    public float FE_Gr_B_Mrks { get; set; }
    public float FE_Gr_C_Mrks { get; set; }
    public float FE_Gr_D_Mrks { get; set; }
    public bool RE_A_Status { get; set; }
    public bool RE_B_Status { get; set; }
    public bool RE_C_Status { get; set; }
    public bool RE_D_Status { get; set; }
    public bool TE_A_Status { get; set; }
    public bool TE_B_Status { get; set; }
    public bool TE_C_Status { get; set; }
    public bool TE_D_Status { get; set; }
    public float FN_Gr_A_Mrks { get; set; }
    public float FN_Gr_B_Mrks { get; set; }
    public float FN_Gr_C_Mrks { get; set; }
    public float FN_Gr_D_Mrks { get; set; }
    public string Roll { get; set; }
    public string Pkt_Type { get; set; }
}
/*  QUERY TABLE --------------------------------------------------------------------*/
public class Student_Info
{
	public String Name{ get; set; }
    [Key]
    [Column(Order = 0)]
	public String RegNo{ get; set; }
    [Key]
    [Column(Order = 1)]
	public int Current_Year{ get; set; }
    [Key]
    [Column(Order = 3)]
	public String Roll{ get; set; }
    [Key]
    [Column(Order = 2)]
	public String Department{ get; set; }
    [Key]
    [Column(Order = 5)]
	public String Subject{ get; set; }
    [Key]
    [Column(Order = 4)]
	public String Sess{ get; set; }
	public String SemCode{ get; set; }
	public double CA{get; set;}
	public double WTG {get; set;}
	public double PLUS {get; set;}
	public double TOTAL {get; set;}
	public string PASS {get; set;}
	public string CREDIT {get; set;}
	public string Remarks {get; set;}
	
}
/************************PassedOutStudentsList**************************************/
[Table("PassedOutStudentsList")]
public class PassedOutStudentsList
{
    [Key]
    public string CertificateNo { get; set; }
    public string IDNo { get; set; }
    public string Student_Id { get; set; }
    public string Roll { get; set; }
    public string Name { get; set; }
    public string PrintName { get; set; }
    public string RegNo { get; set; }
    public string Sex { get; set; }
    public string Sess { get; set; }
    public string Type { get; set; }
    public string DeptCode { get; set; }
    public string Specialisation { get; set; }
    public double? Percentage { get; set; }
    public string Class { get; set; }
    public string Class_Div { get; set; }
    public bool QG { get; set; }
    public string DeptNo { get; set; }
    public int SortOrder { get; set; }
    public string Degree { get; set; }
    public string YearOfPassing { get; set; }
    public string IssueDate { get; set; }
    public bool Improvement { get; set; }
    public string Path { get; set; }
}
/************************student_information****************************************/
[Table("Student_Information")]
public  class Student_Information
{
    [Key]
    [Column(Order = 0)]
    public String Roll { get; set; }
    public string Name { get; set; }
    [Key]
    [Column(Order = 2)]
    public String Sess { get; set; }
     [Key]
    [Column(Order = 3)]
	public String Subject {get; set; }
     [Key]
     [Column(Order = 4)]
     public string SemCode { get; set; }
    public double TOTAL {get; set;}
    public string CREDIT { get; set; }
    public int Campus_Id{ get; set; }
    public double? Crdt { get; set; }
    [Key]
    [Column(Order = 5)]
    public String RegNo { get; set; }
    public string PASS {get; set;}
    [Key]
    [Column(Order = 6)]
    public String Department { get; set; }
}

    
/************************SCRIPT COUNT*****************************/
//[Table("Script_Count")]
//public class Script_Count
//{
//    public string Dept { get; set; }
//    public string Subcode { get; set; }
//    public int Pktno { get; set; }
//    public int No_of_Scripts { get; set; }
//    public string Remark { get; set; }
//    public string Groups { get; set; }
//    public bool Entd { get; set; }
//    public string Sessn { get; set; }
//    public string Paper_Type { get; set; }
//}
/*  QUERY TABLE --------------------------------------------------------------------*/
public class Time_Table_Cnt
{

    public string Department { get; set; }
    public string Semester { get; set; }
    [Key]
    [Column(Order = 1)]
    public String Subject { get; set; }
    public string SubTitle { get; set; }
    [Key]
    [Column(Order = 2)]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
    public DateTime DOE { get; set; }
    [Key]
    [Column(Order = 3)]
    public DateTime Time_From { get; set; }
    public DateTime Time_To { get; set; }
    public string Duration { get; set; }
    public bool Practical { get; set; }
    [Key]
    [Column(Order = 0)]
    public string Sessn { get; set; }
    
    public int T_Count { get; set; }
}
/*  QUERY TABLE --------------------------------------------------------------------*/
public class Measurement_Evaluation
{
    [Key]
    [Column(Order = 0)]
    public string Department { get; set; }
    [Key]
    [Column(Order = 1)]
    public string Semester { get; set; }
    public int Total_Subjects { get; set; }
    public int Total_Scripts { get; set; }
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
    public DateTime Last_Exam { get; set; }
}
}