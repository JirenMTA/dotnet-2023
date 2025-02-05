﻿using System.ComponentModel.DataAnnotations;

namespace LibrarySchool;
/// <summary>
/// Subjects - Class type the subject 
/// </summary>

public class Subject
{
    /// <summary>
    /// SubjectId - Id of subject 
    /// </summary>
    [Key]
    public int SubjectId { get; set; }

    /// <summary>
    /// SubjectName - Name of the subject
    /// </summary>
    public string SubjectName { get; set; } = "";

    public List<Mark> Marks { get; set; } = null!;

    /// <summary>
    /// YearStudy - the year when start study subject
    /// </summary>
    public int YearStudy { get; set; }

    public Subject() { }

    public Subject(int subjectId, string subjectName, int yearStudy, List<Mark> marks)
    {
        SubjectId = subjectId;
        SubjectName = subjectName;
        YearStudy = yearStudy;
        Marks = marks;
    }
}
