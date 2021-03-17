namespace OrdersManager.DAL.Entityes.Base
{
    public abstract class PersonEntityBase: NamedEntityBase
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
