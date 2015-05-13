
using System.ComponentModel;
namespace ArquiteturaBasica.Dominio.Entidade.Base
{
    /// <summary>
    /// A base class for all entities that implements identity equality.
    /// </summary>
    /// <typeparam name="TId">The type of the ID.</typeparam>
    /// <typeparam name="T">The type of the derived class (required for equality)</typeparam>
    public abstract class EntityBase<T> where T : EntityBase<T>, INotifyPropertyChanged
    {
        private int _idd;
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public virtual int ID
        {
            get
            {
                return _idd;

            }
            set
            {
                _idd = value;
                OnPropertyChanged("ID");
            }
        }

        #region Extensibility Method Definitions

        public override bool Equals(object obj)
        {
            T other = obj as T;
            if (other == null)
                return false;

            // handle the case of comparing two NEW objects
            bool otherIsTransient = Equals(other.ID, 0);
            bool thisIsTransient = Equals(ID, 0);
            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.ID.Equals(ID);
        }

        private int? _oldHashCode;

        public override int GetHashCode()
        {
            // Once we have a hash code we'll never change it
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;

            bool thisIsTransient = Equals(ID, 0);

            // When this instance is transient, we use the base GetHashCode()
            // and remember it, so an instance can NEVER change its hash code.
            if (thisIsTransient)
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }
            return ID.GetHashCode();
        }

        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                             new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
