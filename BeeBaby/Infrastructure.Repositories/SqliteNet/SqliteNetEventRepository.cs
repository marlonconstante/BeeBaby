using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;
using System.Text;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetEventRepository: SqliteNetRepositoryBase<Event, EventData>, IEventRepository
	{
		public SqliteNetEventRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetEventMapper(), unitOfWork)
		{
			connection.CreateTable<EventData>();

			if (CountAll(null) <= 0)
			{
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('1', ?, ?, ?, ?)", "Mamãe chegando no hospital para me receber ", 0, 0, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('2', ?, ?, ?, ?)", "Eu nascendo... ", 0, 0, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('3', ?, ?, ?, ?)", "A primeira vez que  Minhã mãe me pegou no colo", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('4', ?, ?, ?, ?)", "A primeira vez que  Mamei", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('5', ?, ?, ?, ?)", "A primeira vez que  Papai me pegou no colo", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('6', ?, ?, ?, ?)", "A primeira vez que  a Vovó me pegou no colo", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('7', ?, ?, ?, ?)", "A primeira vez que  o Vovô me pegou no colo", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('8', ?, ?, ?, ?)", "A primeira vez que  minha familia me viu", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('9', ?, ?, ?, ?)", "A primeira vez que  a Dinda me pegou no colo", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('10', ?, ?, ?, ?)", "A primeira vez que  o Dindo me pegou no colo", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('11', ?, ?, ?, ?)", "Minha primeira Troca de fraldas", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('12', ?, ?, ?, ?)", "A primeira vez que  Abri meus Olhinhos", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('13', ?, ?, ?, ?)", "A primeira vez que  dei um aperto de mão nos dedinhos", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('14', ?, ?, ?, ?)", "Meu primeiro Banho", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('15', ?, ?, ?, ?)", "Meu primeiro Sorriso", 0, 2, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('16', ?, ?, ?, ?)", "Eu no colo da Dra que me fez nascer", 1, 5, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('17', ?, ?, ?, ?)", "Eu no colo da Mamãe", 0, 10, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('18', ?, ?, ?, ?)", "Eu no colo do Papais", 0, 10, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('19', ?, ?, ?, ?)", "Eu no colo do Vovô", 0, 10, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('20', ?, ?, ?, ?)", "Eu no colo da Vovó", 0, 10, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('21', ?, ?, ?, ?)", "Eu no colo da Dinda", 0, 10, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('22', ?, ?, ?, ?)", "Eu no colo do Dindo", 0, 10, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('23', ?, ?, ?, ?)", "Trocando as fraldas ", 0, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('24', ?, ?, ?, ?)", "Meu primeiro Passeio", 3, 30, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('25', ?, ?, ?, ?)", "Minha primeira Gargalhada", 5, 60, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('26', ?, ?, ?, ?)", "Minha primeira Visita na casa dos meus avós", 0, 30, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('27', ?, ?, ?, ?)", "A primeira vez que  tomei Mamadeira", 0, 30, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('28', ?, ?, ?, ?)", "A primeira vez que  coloquei meu Pé na boca", 0, 30, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('29', ?, ?, ?, ?)", "A primeira vez que  chupei meu dedo", 0, 30, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('30', ?, ?, ?, ?)", "Saindo da maternidade... indo pra casa! ", 2, 10, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('31', ?, ?, ?, ?)", "Eu chegando em casa a primeira vez ", 2, 10, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('32', ?, ?, ?, ?)", "Meu primeiro Soninho no meu Berço", 2, 10, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('33', ?, ?, ?, ?)", "Meu primeiro Banho de banheira", 2, 20, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('34', ?, ?, ?, ?)", "Recebendo visita em casa ", 2, 60, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('35', ?, ?, ?, ?)", "Bocejando... ", 0, 20, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('36', ?, ?, ?, ?)", "Dormindo gostoso... ", 0, 30, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('37', ?, ?, ?, ?)", "Aperto de mão de bebê... ", 0, 30, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('38', ?, ?, ?, ?)", "Meu time do coração ", 3, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('39', ?, ?, ?, ?)", "Brincando na cama ", 3, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('40', ?, ?, ?, ?)", "Rindo, sorrindo, me divertindo ", 3, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('41', ?, ?, ?, ?)", "Retratos com quem mais amo ", 3, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('42', ?, ?, ?, ?)", "Meu primeiro Natal", 0, 365, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('43', ?, ?, ?, ?)", "Meu primeiro Brinquedinho", 3, 20, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('44', ?, ?, ?, ?)", "Como é bom um brinquedinho ", 3, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('45', ?, ?, ?, ?)", "Nenhum momento. Apenas minha cara linda! ", 0, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('46', ?, ?, ?, ?)", "Minha primeira Praia", 15, 360, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('47', ?, ?, ?, ?)", "Eu passeando Na praia", 15, 360, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('48', ?, ?, ?, ?)", "Eu passeando No parque", 15, 360, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('49', ?, ?, ?, ?)", "Eu passeando Na casa dos meus avós", 5, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('50', ?, ?, ?, ?)", "Momentos com papai...", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('51', ?, ?, ?, ?)", "Momentos com mamãe...", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('52', ?, ?, ?, ?)", "Momentos com meus pais...", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('53', ?, ?, ?, ?)", "Momentos com meu Vovô", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('54', ?, ?, ?, ?)", "Momentos com minha Vovó", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('55', ?, ?, ?, ?)", "Momentos com minha Dinda", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('56', ?, ?, ?, ?)", "Momentos com meu Dindo", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('57', ?, ?, ?, ?)", "Momentos com amigos... ", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('58', ?, ?, ?, ?)", "Momentos em familia... ", 10, 90, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('59', ?, ?, ?, ?)", "Eu com 100 horas de vida! ", 3, 5, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('60', ?, ?, ?, ?)", "Eu com 1000 horas de vida! ", 40, 43, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('61', ?, ?, ?, ?)", "Meu Primeiro Mês de vida ", 28, 32, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('62', ?, ?, ?, ?)", "Meu Segundo mês de vida ", 58, 63, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('63', ?, ?, ?, ?)", "Meu Terceiro mês de vida ", 88, 92, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('64', ?, ?, ?, ?)", "Meu Quarto mês de vida ", 118, 123, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('65', ?, ?, ?, ?)", "Meu Quinto mês de vida ", 148, 153, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('66', ?, ?, ?, ?)", "Meu Sexto mês de vida ", 178, 182, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('67', ?, ?, ?, ?)", "Meu Sétimo mês de vida ", 208, 212, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('68', ?, ?, ?, ?)", "Meu Oitavo mês de vida ", 238, 242, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('69', ?, ?, ?, ?)", "Meu Novo mês de vida ", 268, 272, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('70', ?, ?, ?, ?)", "Meu Décimo mês de vida ", 298, 302, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('71', ?, ?, ?, ?)", "Meu Décimo Primeiro mês de vida ", 328, 333, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind) values ('72', ?, ?, ?, ?)", "Meu Primeiro Aninho! ", 358, 362, 1);
			}
		}
	}
}

