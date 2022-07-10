namespace GradeCenter.Server.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;

    public class EditTeacherSubjectsInputModel
    {
        public EditTeacherSubjectsInputModel()
        {
            this.subjects = new List<EditSubjectInputModel>();
        }

        public List<EditSubjectInputModel> subjects { get; set; }
    }
}
