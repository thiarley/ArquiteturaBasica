using ArquiteturaBasica.Dominio.Entidade;
using FluentNHibernate.Mapping;

namespace Repository.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("usuario");

            Not.LazyLoad();

            Id(x => x.ID)
              .Column("ID")
              .CustomType("Int32")              
              .Not.Nullable()
              .Precision(10)
              .GeneratedBy.Identity();

            Map(x => x.Login)
              .Column("Login")
              .CustomType("String")             
              .Not.Nullable()
              .Length(45)
              .Unique();

            Map(x => x.Senha)
              .Column("Senha")
              .CustomType("String")             
              .Not.Nullable()
              .Length(256);

            Map(x => x.Administrador)
              .Column("Administrador")
              .CustomType("Boolean")
              .Default("'0'")             
              .Not.Nullable()
              .Precision(1);

            Map(x => x.Bloqueado)
              .Column("Bloqueado")
              .CustomType("Boolean")
              .Default("'0'")             
              .Not.Nullable()
              .Precision(1);

            Map(x => x.Nome)
              .Column("Nome")
              .CustomType("String")             
              .Not.Nullable()
              .Length(300);

            Map(x => x.Email)
              .Column("Email")
              .CustomType("String")             
              .Not.Nullable()
              .Length(300);
        }
    }

}
