using System;
using MinhLam.Framework.Data;

namespace MinhLam.Framework.Application
{
    [Serializable]
    public abstract class BaseModel
    {
        public BaseModel() { }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get;  set; }

        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// The property is used when binding to a control.
        /// </summary>
        public string DisplayText
        {
            get { return GetDisplayText(); }
        }

        #region Abstract Methods

        /// <summary>
        /// Get the record from the database and load the object's properties
        /// </summary>
        /// <returns>Returns true if the record is found.</returns>
        public abstract bool Load(object id);

        /// <summary>
        /// This method will map the fields in the data reader to the member variables in the object.
        /// </summary>
        protected abstract void MapEntityToCustomProperties(BaseEntity entity);

        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// </summary>
        protected abstract string GetDisplayText();

        /// <summary>
        /// This will load the object with the default properties.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// This method will add or update a record.
        /// </summary>
        public abstract bool Save(ref ValidationErrors validationErrors, string userName);

        /// <summary>
        /// This method validates the object's data before trying to save the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void Validate(ref ValidationErrors validationErrors);

        /// <summary>
        /// This should call the business object's data class to delete the record.  The only method that should call this 
        /// is the virtual method "Delete(SqlTransaction tn, ref ValidationErrorAL validationErrors, int id)" in this class.
        /// </summary>
        protected abstract void DeleteForReal();

        protected abstract bool IsNewRecord();


        /// <summary>
        /// Deletes the record.
        /// </summary>
        public virtual bool Delete(ref ValidationErrors validationErrors)
        {
            
            //Check if this record can be deleted.  There may be referential integrity rules preventing it from being deleted                
            ValidateDelete(ref validationErrors);

            if (validationErrors.Count == 0)
            {
                this.DeleteForReal();
                return true;
            }
            else
            {
                //The record can not be deleted.
                return false;
            }
            
        }

        /// <summary>
        /// This method validates the object's data before trying to delete the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void ValidateDelete(ref ValidationErrors validationErrors);

        #endregion Abstratct Methods

        /// <summary>
        /// This method will load all the properties of the object from the entity.
        /// </summary>        
        public void MapEntityToProperties(BaseEntity entity)
        {
            if (entity != null)
            {
                CreatedDate = entity.CreatedDate;
                CreatedBy = entity.CreatedBy;
                UpdatedDate = entity.UpdatedDate;
                UpdatedBy = entity.UpdatedBy;
                this.MapEntityToCustomProperties(entity);
            }
        }
    }
}
