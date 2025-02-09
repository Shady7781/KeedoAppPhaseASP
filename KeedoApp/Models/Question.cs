﻿using System;

namespace KeedoApp.Models
{




	[Serializable]
	public class Question
	{
		private const long serialVersionUID = 1L;
		private int id;


		private string questions;


		private QuestionType type;

		private Response response;
		public int? feedbackFk { get; set; }
		private Feedback feedback;
		private DateTime createdAt;
		private Nullable<DateTime> updatedAt;

		public virtual int Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}
		public virtual string Questions
		{
			get
			{
				return questions;
			}
			set
			{
				this.questions = value;
			}
		}



		public virtual QuestionType Type
		{
			get
			{
				return type;
			}
			set
			{
				this.type = value;
			}
		}

		public Response Response
		{
			get
			{
				return response;
			}
			set
			{
				this.response = value;
			}
		}
		public virtual Feedback Feedback
		{
			get
			{
				return feedback;
			}
			set
			{
				this.feedback = value;
			}
		}
		public static long Serialversionuid
		{
			get
			{
				return serialVersionUID;
			}
		}
		public virtual DateTime CreatedAt
		{
			get
			{
				return createdAt;
			}
			set
			{
				this.createdAt = value;
			}
		}


		public virtual Nullable<DateTime> UpdatedAt
		{
			get
			{
				return updatedAt;
			}
			set
			{
				this.updatedAt = value;
			}
		}
















	}
}
