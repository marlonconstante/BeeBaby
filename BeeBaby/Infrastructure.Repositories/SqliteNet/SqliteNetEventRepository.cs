using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;
using System.Text;
using System.Collections.Generic;
using Infrastructure.Repositories.Commons;
using Infrastructure.Configuration;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetEventRepository: SqliteNetRepositoryBase<Event, EventData>, IEventRepository
	{
		public SqliteNetEventRepository(SQLiteConnection connection, IUnitOfWork unitOfWork, string currentCulture) : base(connection, new SqliteNetEventMapper(), unitOfWork)
		{
			DataVersion = 1;

			#region CreateData
			connection.CreateTable<SystemParameterData>();
			connection.CreateTable<EventData>();

			if (CountAll(null) <= 0 || !SystemParameterService.IsDataStructureUpdate(typeof(EventData).Name, DataVersion) || SystemParameterService.HasCurrentCultureChanged(currentCulture))
			{
				connection.DeleteAll<EventData>();
				PopulateData(connection, currentCulture);
				SystemParameterService.SaveEntityVersion(typeof(EventData).Name, DataVersion.ToString());
			}
			#endregion
		}

		/// <summary>
		/// Finds the events with non used achivments.
		/// </summary>
		/// <returns>The events with non used achivments.</returns>
		public IEnumerable<Event> FindEventsWithNonUsedAchievements()
		{
			var events = m_connection.Query<EventData>("select * from EventData E " +
			             "where (Kind = 0 and not exists (select 1 from MomentData where E.Id = EventId)) " +
			             "or (Kind = 1) Order by Priority");

			return MapperHelper.ToDomainEntities<Event, EventData>(events, Mapper);
		}

		/// <summary>
		/// Determines whether this instance has current culture been changed.
		/// </summary>
		/// <returns><c>true</c> if this instance has current culture been changed; otherwise, <c>false</c>.</returns>
		bool HasCurrentCultureBeenChanged()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Populates the data.
		/// </summary>
		/// <param name="connection">Connection.</param>
		/// <param name="currentCultureName">Current culture name.</param>
		static void PopulateData(SQLiteConnection connection, string currentCultureName)
		{
			if (currentCultureName.Equals("pt-BR"))
			{
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('1', ?, ?, ?, ?, ?, ?)", "Mamãe fazendo exercício comigo na barriga", -999, -90, 1, 9, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('2', ?, ?, ?, ?, ?, ?)", "Na barriga da mamãe", -999, -1, 1, 9, 2);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('3', ?, ?, ?, ?, ?, ?)", "Dia das mães na barriga", -999, -1, 0, 9, 3);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('4', ?, ?, ?, ?, ?, ?)", "Familia reunida em volta da barriga", -999, -1, 1, 9, 4);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('5', ?, ?, ?, ?, ?, ?)", "Ganhando beijo na barriga", -999, -1, 1, 9, 5);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('6', ?, ?, ?, ?, ?, ?)", "Me escutando na barriga da mamãe", -999, -5, 1, 9, 6);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('7', ?, ?, ?, ?, ?, ?)", "Lendo revistas de bebês", -999, -5, 1, 9, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('8', ?, ?, ?, ?, ?, ?)", "Primeiro sapatinho", -999, -15, 0, 9, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('9', ?, ?, ?, ?, ?, ?)", "Primeira roupinha", -999, -15, 0, 9, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('10', ?, ?, ?, ?, ?, ?)", "Primeiro brinquedinho", -999, -15, 0, 9, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('11', ?, ?, ?, ?, ?, ?)", "Primeira chupeta", -999, -15, 0, 9, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('12', ?, ?, ?, ?, ?, ?)", "Preparando meu quartinho", -999, -15, 1, 9, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('13', ?, ?, ?, ?, ?, ?)", "Comprando coisas do meu enxoval", -999, -15, 1, 9, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('14', ?, ?, ?, ?, ?, ?)", "Dando chutes na barriga da mamãe", -120, -1, 1, 9, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('15', ?, ?, ?, ?, ?, ?)", "Me espreguiçando na barriga da mamãe", -120, -1, 1, 9, 15);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('16', ?, ?, ?, ?, ?, ?)", "Preparando meu chá de fraldas", -90, -15, 0, 9, 16);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('17', ?, ?, ?, ?, ?, ?)", "Meu chá de fraldas", -90, -15, 0, 9, 17);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('18', ?, ?, ?, ?, ?, ?)", "Um book profissional de gestante", -90, -15, 1, 9, 18);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('19', ?, ?, ?, ?, ?, ?)", "Indo para maternidade", -15, 0, 0, 9, 19);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('20', ?, ?, ?, ?, ?, ?)", "Preparando minha mala para maternidade", -15, 0, 0, 9, 20);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('21', ?, ?, ?, ?, ?, ?)", "Ultima foto em familia antes de eu nascer", -5, 0, 0, 9, 21);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('22', ?, ?, ?, ?, ?, ?)", "Mamãe preparada para eu nascer", -5, 0, 1, 9, 22);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('23', ?, ?, ?, ?, ?, ?)", "Saindo da barriga da mamãe", -5, 0, 0, 8, 23);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('24', ?, ?, ?, ?, ?, ?)", "Mamãe chegando no hospital para me receber", -5, 0, 0, 8, 24);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('25', ?, ?, ?, ?, ?, ?)", "Meu 1o Banho", -2, 2, 0, 4, 25);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('26', ?, ?, ?, ?, ?, ?)", "Vendo meu peso", -2, 2, 0, 8, 26);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('27', ?, ?, ?, ?, ?, ?)", "Meu umbiguinho", -2, 2, 1, 8, 27);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('28', ?, ?, ?, ?, ?, ?)", "Mamãe me pegou no colo", 0, 2, 0, 6, 28);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('29', ?, ?, ?, ?, ?, ?)", "Papai me pegou no colo", 0, 2, 0, 6, 29);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('30', ?, ?, ?, ?, ?, ?)", "Vovó me pegou no colo", 0, 2, 0, 6, 30);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('31', ?, ?, ?, ?, ?, ?)", "Vovô me pegou no colo", 0, 2, 0, 6, 31);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('32', ?, ?, ?, ?, ?, ?)", "Madrinha me pegou no colo", 0, 2, 0, 6, 32);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('33', ?, ?, ?, ?, ?, ?)", "Padrinho  me pegou no colo", 0, 2, 0, 6, 33);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('34', ?, ?, ?, ?, ?, ?)", "Dei um aperto de mão nos dedinhos", 0, 2, 0, 0, 34);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('35', ?, ?, ?, ?, ?, ?)", "Abri meus Olhinhos", 0, 2, 0, 0, 35);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('36', ?, ?, ?, ?, ?, ?)", "Troquei de Fraldas", 0, 2, 0, 0, 36);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('37', ?, ?, ?, ?, ?, ?)", "Quando minha familia me viu", 0, 2, 0, 1, 37);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('38', ?, ?, ?, ?, ?, ?)", "Primeira mamada", 0, 2, 0, 8, 38);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('39', ?, ?, ?, ?, ?, ?)", "Recebendo visita na maternidade", 0, 5, 1, 1, 39);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('40', ?, ?, ?, ?, ?, ?)", "No colo da Mamãe", 0, 10, 1, 6, 40);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('41', ?, ?, ?, ?, ?, ?)", "No colo do Papai", 0, 10, 1, 6, 41);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('42', ?, ?, ?, ?, ?, ?)", "No colo do Vovô", 0, 10, 1, 6, 42);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('43', ?, ?, ?, ?, ?, ?)", "No colo da Vovó", 0, 10, 1, 6, 43);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('44', ?, ?, ?, ?, ?, ?)", "No colo da Madrinha", 0, 10, 1, 6, 44);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('45', ?, ?, ?, ?, ?, ?)", "No colo do Padrinho", 0, 10, 1, 6, 45);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('46', ?, ?, ?, ?, ?, ?)", "Só um chorinho...", 0, 15, 1, 5, 39);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('47', ?, ?, ?, ?, ?, ?)", "Aperto de mão de bebê...", 0, 30, 1, 0, 40);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('48', ?, ?, ?, ?, ?, ?)", "Tomei injeção", 0, 30, 0, 0, 41);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('49', ?, ?, ?, ?, ?, ?)", "Bocejando...", 0, 30, 1, 3, 42);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('50', ?, ?, ?, ?, ?, ?)", "Dormindo gostoso...", 0, 30, 1, 3, 43);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('51', ?, ?, ?, ?, ?, ?)", "Meu 1o Sorriso", 0, 30, 0, 5, 44);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('52', ?, ?, ?, ?, ?, ?)", "Cortei as unhas", 1, 30, 0, 0, 45);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('53', ?, ?, ?, ?, ?, ?)", "Cortei as unhas", 1, 30, 0, 3, 46);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('54', ?, ?, ?, ?, ?, ?)", "Eu tomando banho de gato", 2, 10, 1, 4, 47);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('55', ?, ?, ?, ?, ?, ?)", "Saindo da maternidade... indo pra casa!", 2, 10, 0, 2, 48);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('56', ?, ?, ?, ?, ?, ?)", "Cheguei em casa", 2, 10, 0, 2, 49);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('57', ?, ?, ?, ?, ?, ?)", "Tirei um soninho no meu berço", 2, 10, 0, 3, 50);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('58', ?, ?, ?, ?, ?, ?)", "Tomei banho de banheira", 2, 20, 0, 4, 51);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('59', ?, ?, ?, ?, ?, ?)", "Eu com 100 horas de vida!", 3, 5, 0, 7, 52);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('60', ?, ?, ?, ?, ?, ?)", "Meu 1o Passeio", 3, 30, 0, 2, 53);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('61', ?, ?, ?, ?, ?, ?)", "visitei a casa dos meus avós", 3, 30, 0, 2, 54);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('62', ?, ?, ?, ?, ?, ?)", "Quando caiu meu umbiguinho", 7, 20, 0, 0, 55);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('63', ?, ?, ?, ?, ?, ?)", "Junto com meu primeiro brinquedinho", 5, 30, 1, 1, 56);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('64', ?, ?, ?, ?, ?, ?)", "Nú artistico!", 5, 30, 1, 0, 57);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('65', ?, ?, ?, ?, ?, ?)", "Meu pezinho", 5, 30, 1, 0, 58);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('66', ?, ?, ?, ?, ?, ?)", "Minha mãozinha", 5, 30, 1, 0, 59);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('67', ?, ?, ?, ?, ?, ?)", "Minha boquinha", 5, 30, 1, 0, 60);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('68', ?, ?, ?, ?, ?, ?)", "Usei chupeta", 5, 30, 0, 0, 61);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('69', ?, ?, ?, ?, ?, ?)", "Cortando as unhas", 5, 30, 1, 0, 62);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('70', ?, ?, ?, ?, ?, ?)", "Fui no pediatra", 5, 30, 0, 0, 63);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('71', ?, ?, ?, ?, ?, ?)", "Eu tomando banho", 10, 30, 1, 4, 64);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('72', ?, ?, ?, ?, ?, ?)", "To chorando, mas logo passa", 16, 30, 1, 5, 65);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('73', ?, ?, ?, ?, ?, ?)", "Recebendo visita em casa", 2, 60, 1, 1, 66);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('74', ?, ?, ?, ?, ?, ?)", "Passeando no carrinho", 5, 60, 1, 2, 67);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('75', ?, ?, ?, ?, ?, ?)", "chupando minha chupeta", 15, 45, 1, 0, 68);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('76', ?, ?, ?, ?, ?, ?)", "Fui passear na pracinha", 15, 60, 0, 2, 69);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('77', ?, ?, ?, ?, ?, ?)", "coloquei meu pé na boca", 20, 45, 0, 0, 70);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('78', ?, ?, ?, ?, ?, ?)", "chupei meu dedo", 20, 45, 0, 0, 71);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('79', ?, ?, ?, ?, ?, ?)", "tomei mamadeira", 20, 45, 0, 0, 72);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('80', ?, ?, ?, ?, ?, ?)", "Comendo meu pezinho", 20, 45, 1, 0, 73);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('81', ?, ?, ?, ?, ?, ?)", "chupando os dedinhos", 20, 45, 1, 0, 74);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('82', ?, ?, ?, ?, ?, ?)", "Curtindo uma música", 20, 45, 1, 0, 75);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('83', ?, ?, ?, ?, ?, ?)", "Meu Primeiro mensário", 28, 32, 0, 7, 76);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('84', ?, ?, ?, ?, ?, ?)", "Eu com 1000 horas de vida!", 40, 43, 0, 7, 77);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('85', ?, ?, ?, ?, ?, ?)", "Trocando as fraldas", 0, 90, 1, 0, 78);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('86', ?, ?, ?, ?, ?, ?)", "Mamando feliz da vida", 0, 120, 1, 1, 78);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('87', ?, ?, ?, ?, ?, ?)", "Retratos com quem mais amo", 3, 90, 1, 1, 79);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('88', ?, ?, ?, ?, ?, ?)", "Momento Rei Leão", 5, 90, 1, 1, 80);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('89', ?, ?, ?, ?, ?, ?)", "Nenhum momento. Apenas minha cara linda!", 5, 90, 1, 1, 81);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('90', ?, ?, ?, ?, ?, ?)", "na igreja", 5, 90, 0, 2, 82);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('91', ?, ?, ?, ?, ?, ?)", "Fui num shopping", 5, 90, 0, 2, 83);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('92', ?, ?, ?, ?, ?, ?)", "Fui num supermercado", 5, 90, 0, 2, 84);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('93', ?, ?, ?, ?, ?, ?)", "Passeando de carro", 5, 90, 1, 2, 85);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('94', ?, ?, ?, ?, ?, ?)", "Dormindo no carrinho", 5, 90, 1, 3, 86);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('95', ?, ?, ?, ?, ?, ?)", "Soninho no carro", 5, 90, 1, 3, 87);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('96', ?, ?, ?, ?, ?, ?)", "Meu time do coração", 10, 90, 1, 1, 88);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('97', ?, ?, ?, ?, ?, ?)", "Visita no pediatra", 15, 90, 1, 0, 89);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('98', ?, ?, ?, ?, ?, ?)", "Brincando na cama", 30, 90, 1, 1, 90);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('99', ?, ?, ?, ?, ?, ?)", "Rindo, sorrindo, me divertindo", 45, 90, 1, 5, 91);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('100', ?, ?, ?, ?, ?, ?)", "Minha 1a risada", 45, 90, 0, 5, 92);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('101', ?, ?, ?, ?, ?, ?)", "Meu Segundo mensário", 58, 63, 0, 7, 93);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('102', ?, ?, ?, ?, ?, ?)", "Soneca com papai", 1, 120, 1, 3, 94);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('103', ?, ?, ?, ?, ?, ?)", "Soneca com mamãe", 1, 120, 1, 3, 95);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('104', ?, ?, ?, ?, ?, ?)", "Passeando como um canguru", 15, 120, 1, 2, 96);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('105', ?, ?, ?, ?, ?, ?)", "Segurei um objeto", 30, 120, 0, 0, 97);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('106', ?, ?, ?, ?, ?, ?)", "Acompanhando movimentos", 30, 120, 1, 0, 98);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('107', ?, ?, ?, ?, ?, ?)", "Momentos com amigos", 30, 120, 1, 7, 99);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('108', ?, ?, ?, ?, ?, ?)", "Momentos com papai", 30, 120, 1, 1, 100);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('109', ?, ?, ?, ?, ?, ?)", "Momentos com mamãe", 30, 120, 1, 1, 101);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('110', ?, ?, ?, ?, ?, ?)", "Momentos com meus pais", 30, 120, 1, 1, 102);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('111', ?, ?, ?, ?, ?, ?)", "Momentos com vovô", 30, 120, 1, 1, 103);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('112', ?, ?, ?, ?, ?, ?)", "Momentos com vovó", 30, 120, 1, 1, 104);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('113', ?, ?, ?, ?, ?, ?)", "Momentos com meus padrinhos", 30, 120, 1, 1, 105);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('114', ?, ?, ?, ?, ?, ?)", "Momentos em familia", 30, 120, 1, 1, 106);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('115', ?, ?, ?, ?, ?, ?)", "Passeio na casa dos meus avós", 30, 120, 1, 2, 107);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('116', ?, ?, ?, ?, ?, ?)", "Levantei a cabecinha", 60, 120, 0, 0, 108);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('117', ?, ?, ?, ?, ?, ?)", "Acompanhei movimentos", 60, 120, 0, 0, 109);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('118', ?, ?, ?, ?, ?, ?)", "Me virei", 60, 120, 0, 0, 110);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('119', ?, ?, ?, ?, ?, ?)", "Fazendo careta para comidinhas novas", 60, 120, 1, 0, 111);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('120', ?, ?, ?, ?, ?, ?)", "Comecei a gargalhar", 60, 120, 0, 5, 112);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('121', ?, ?, ?, ?, ?, ?)", "Meu Terceiro mensário", 88, 92, 0, 7, 113);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('122', ?, ?, ?, ?, ?, ?)", "Tomei banho de mar", 15, 150, 0, 4, 114);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('123', ?, ?, ?, ?, ?, ?)", "Tomei banho de sol", 15, 150, 0, 4, 115);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('124', ?, ?, ?, ?, ?, ?)", "Meu Quarto mensário", 118, 123, 0, 7, 116);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('125', ?, ?, ?, ?, ?, ?)", "assistindo a missa", 15, 180, 1, 2, 117);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('126', ?, ?, ?, ?, ?, ?)", "Passeando no shopping", 15, 180, 1, 2, 118);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('127', ?, ?, ?, ?, ?, ?)", "Compras no supermercado", 15, 180, 1, 2, 119);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('128', ?, ?, ?, ?, ?, ?)", "Fui ver um filme no cinema", 30, 180, 0, 2, 120);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('129', ?, ?, ?, ?, ?, ?)", "Machucadinho", 60, 180, 0, 0, 121);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('130', ?, ?, ?, ?, ?, ?)", "Consegui sentar", 90, 180, 0, 0, 122);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('131', ?, ?, ?, ?, ?, ?)", "Tentando sentar", 90, 180, 1, 0, 123);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('132', ?, ?, ?, ?, ?, ?)", "Assistindo televisão", 180, 720, 1, 1, 124);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('133', ?, ?, ?, ?, ?, ?)", "Tentando falar ao telefone", 360, 720, 1, 1, 125);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('134', ?, ?, ?, ?, ?, ?)", "Brincando no parquinho", 360, 720, 1, 2, 126);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('135', ?, ?, ?, ?, ?, ?)", "Curtindo a festinha", 360, 720, 1, 2, 127);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('136', ?, ?, ?, ?, ?, ?)", "Caindo de sono", 360, 720, 1, 3, 128);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('137', ?, ?, ?, ?, ?, ?)", "No restaurante", 90, 180, 0, 2, 129);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('138', ?, ?, ?, ?, ?, ?)", "Curtindo no restaurante", 180, 360, 1, 2, 130);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('139', ?, ?, ?, ?, ?, ?)", "Brincando com minha irmã", 180, 720, 1, 1, 131);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('140', ?, ?, ?, ?, ?, ?)", "Brincando com meu irmão", 180, 720, 1, 1, 132);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('141', ?, ?, ?, ?, ?, ?)", "Brincando com amiguinhos", 180, 720, 1, 2, 133);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('142', ?, ?, ?, ?, ?, ?)", "Comendo o que não devia", 180, 720, 1, 0, 134);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('143', ?, ?, ?, ?, ?, ?)", "Meu Quinto mensário", 148, 153, 0, 7, 135);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('144', ?, ?, ?, ?, ?, ?)", "Meu Sexto mensário", 178, 182, 0, 7, 136);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('145', ?, ?, ?, ?, ?, ?)", "Meu Sétimo mensário", 208, 212, 0, 7, 137);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('146', ?, ?, ?, ?, ?, ?)", "Meu Oitavo mensário", 238, 242, 0, 7, 138);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('147', ?, ?, ?, ?, ?, ?)", "Meu Novo mensário", 268, 272, 0, 7, 139);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('148', ?, ?, ?, ?, ?, ?)", "Meu Décimo mensário", 298, 302, 0, 7, 140);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('149', ?, ?, ?, ?, ?, ?)", "Tomando vacina", 15, 360, 1, 0, 141);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('150', ?, ?, ?, ?, ?, ?)", "Meu batizado", 30, 360, 0, 7, 142);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('151', ?, ?, ?, ?, ?, ?)", "Passeio no parque", 30, 360, 1, 2, 143);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('152', ?, ?, ?, ?, ?, ?)", "Fui para praia", 30, 360, 0, 2, 144);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('153', ?, ?, ?, ?, ?, ?)", "Passeio na praia", 30, 360, 1, 2, 145);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('154', ?, ?, ?, ?, ?, ?)", "Tomei banho de piscina", 60, 360, 0, 4, 146);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('155', ?, ?, ?, ?, ?, ?)", "Banho de piscina", 60, 360, 1, 4, 147);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('156', ?, ?, ?, ?, ?, ?)", "comi papinha", 90, 360, 0, 0, 148);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('157', ?, ?, ?, ?, ?, ?)", "Comendo papinha", 90, 360, 1, 0, 149);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('158', ?, ?, ?, ?, ?, ?)", "Fui no zoológico", 90, 360, 0, 2, 150);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('159', ?, ?, ?, ?, ?, ?)", "Brincando na pracinha", 90, 360, 1, 2, 151);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('160', ?, ?, ?, ?, ?, ?)", "Tentei falar e só saiu barulho", 120, 360, 0, 0, 152);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('161', ?, ?, ?, ?, ?, ?)", "fiz gracinha", 120, 360, 0, 0, 153);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('162', ?, ?, ?, ?, ?, ?)", "Comecei a engatinhar", 120, 360, 0, 0, 154);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('163', ?, ?, ?, ?, ?, ?)", "Falando a lingua de bebês", 120, 360, 1, 0, 155);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('164', ?, ?, ?, ?, ?, ?)", "Engatinhando pela casa", 120, 720, 1, 0, 156);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('165', ?, ?, ?, ?, ?, ?)", "Engatinhando pelo mundo", 120, 720, 1, 0, 157);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('166', ?, ?, ?, ?, ?, ?)", "Começou a nascer um dentinho", 150, 720, 0, 0, 158);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('167', ?, ?, ?, ?, ?, ?)", "Fiquei de pé", 150, 720, 0, 0, 159);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('168', ?, ?, ?, ?, ?, ?)", "Dentinhos crescendo", 150, 360, 1, 0, 160);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('169', ?, ?, ?, ?, ?, ?)", "Tentando caminhar", 150, 360, 1, 0, 161);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('170', ?, ?, ?, ?, ?, ?)", "Bati palminha", 180, 360, 0, 0, 162);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('171', ?, ?, ?, ?, ?, ?)", "Batendo palminha", 180, 360, 1, 0, 163);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('172', ?, ?, ?, ?, ?, ?)", "Dodói mas logo passa", 180, 720, 1, 0, 164);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('173', ?, ?, ?, ?, ?, ?)", "Fui em um circo", 180, 360, 0, 2, 165);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('174', ?, ?, ?, ?, ?, ?)", "Cheio de protetor solar", 180, 360, 1, 2, 166);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('175', ?, ?, ?, ?, ?, ?)", "Meu Décimo Primeiro mensário", 328, 333, 0, 7, 167);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('176', ?, ?, ?, ?, ?, ?)", "Meu Primeiro Aninho!", 358, 362, 0, 7, 168);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('177', ?, ?, ?, ?, ?, ?)", "fiz uma viagem", 120, 420, 0, 7, 169);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('178', ?, ?, ?, ?, ?, ?)", "Falei uma palavra que dava pra entender", 120, 720, 0, 0, 170);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('179', ?, ?, ?, ?, ?, ?)", "falei mamãe", 120, 240, 0, 0, 171);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('180', ?, ?, ?, ?, ?, ?)", "falei papai", 120, 240, 0, 0, 172);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('181', ?, ?, ?, ?, ?, ?)", "Cortei os cabelos", 120, 500, 0, 0, 173);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('182', ?, ?, ?, ?, ?, ?)", "Cortando meus cabelos", 120, 240, 1, 0, 174);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('183', ?, ?, ?, ?, ?, ?)", "Tomando injeção", 90, 180, 1, 0, 175);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('184', ?, ?, ?, ?, ?, ?)", "Eu e minha babá", 90, 180, 1, 10, 176);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('185', ?, ?, ?, ?, ?, ?)", "Primeiro dia na creche", 180, 720, 0, 10, 177);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('186', ?, ?, ?, ?, ?, ?)", "Me preparando para creche", 720, 720, 1, 10, 178);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('187', ?, ?, ?, ?, ?, ?)", "Festinha na creche", 180, 720, 1, 10, 179);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('188', ?, ?, ?, ?, ?, ?)", "Mamãe me visitando na creche", 180, 720, 1, 10, 180);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('189', ?, ?, ?, ?, ?, ?)", "Passeando no zoológico", 180, 720, 1, 2, 181);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('190', ?, ?, ?, ?, ?, ?)", "Fui em um museu", 360, 720, 0, 2, 182);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('191', ?, ?, ?, ?, ?, ?)", "Fui no teatro", 360, 720, 0, 2, 183);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('192', ?, ?, ?, ?, ?, ?)", "Vendo filme no cinema", 360, 720, 1, 2, 184);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('193', ?, ?, ?, ?, ?, ?)", "Passeando no circo", 360, 720, 1, 2, 185);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('194', ?, ?, ?, ?, ?, ?)", "Meu 1o Natal", 999, 999, 0, 7, 186);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('195', ?, ?, ?, ?, ?, ?)", "Meu 1o dia das mães", 999, 999, 0, 7, 187);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('196', ?, ?, ?, ?, ?, ?)", "Dia das mães!", 999, 999, 1, 7, 188);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('197', ?, ?, ?, ?, ?, ?)", "Meu 1o dia dos pais", 999, 999, 0, 7, 189);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('198', ?, ?, ?, ?, ?, ?)", "Dia dos pais!", 999, 999, 1, 7, 190);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('199', ?, ?, ?, ?, ?, ?)", "Andei de avião", 999, 999, 0, 2, 188);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('200', ?, ?, ?, ?, ?, ?)", "Primeiro dia na escolinha", 720, 1440, 0, 10, 189);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('201', ?, ?, ?, ?, ?, ?)", "Minha mochila", 720, 1440, 0, 10, 190);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('202', ?, ?, ?, ?, ?, ?)", "Me preparando para escolinha", 720, 1440, 1, 10, 191);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('203', ?, ?, ?, ?, ?, ?)", "Festinha na escolinha", 720, 1440, 1, 10, 192);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('204', ?, ?, ?, ?, ?, ?)", "Assistindo um teatro", 1440, 2880, 1, 2, 193);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('205', ?, ?, ?, ?, ?, ?)", "Dente mole", 1620, 3240, 0, 0, 194);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('206', ?, ?, ?, ?, ?, ?)", "Caiu meu dente", 1620, 3240, 0, 0, 195);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('207', ?, ?, ?, ?, ?, ?)", "Mais um dente caindo", 1620, 3240, 1, 0, 196);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('208', ?, ?, ?, ?, ?, ?)", "Fazendo trabalhinho da escola", 720, 1440, 1, 10, 197);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('209', ?, ?, ?, ?, ?, ?)", "Festa de 1 aninho", 345, 390, 0, 7, 198);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('210', ?, ?, ?, ?, ?, ?)", "Festa de 2 aninhos", 690, 780, 0, 7, 199);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('211', ?, ?, ?, ?, ?, ?)", "Festa de 3 aninhos", 1380, 1560, 0, 7, 200);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('212', ?, ?, ?, ?, ?, ?)", "Festa de 4 aninhos", 2760, 3120, 0, 7, 201);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('213', ?, ?, ?, ?, ?, ?)", "Festinha de aniversário", 345, 3120, 1, 7, 202);
			}
			else
			{

				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('1', ?, ?, ?, ?, ?, ?)", "Mother doing exercise with me in the belly ", -999, -90, 1, 9, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('2', ?, ?, ?, ?, ?, ?)", "In Mommy's tummy ", -999, -1, 1, 9, 2);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('3', ?, ?, ?, ?, ?, ?)", "Mothers Day in the belly ", -999, -1, 0, 9, 3);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('4', ?, ?, ?, ?, ?, ?)", "Family gathered around the belly ", -999, -1, 1, 9, 4);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('5', ?, ?, ?, ?, ?, ?)", "Earning kiss on the belly ", -999, -1, 1, 9, 5);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('6', ?, ?, ?, ?, ?, ?)", "Hear me in Mommy's tummy ", -999, -5, 1, 9, 6);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('7', ?, ?, ?, ?, ?, ?)", "Reading magazines babies ", -999, -5, 1, 9, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('8', ?, ?, ?, ?, ?, ?)", "first slipper ", -999, -15, 0, 9, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('9', ?, ?, ?, ?, ?, ?)", "first outfit ", -999, -15, 0, 9, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('10', ?, ?, ?, ?, ?, ?)", "first toy ", -999, -15, 0, 9, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('11', ?, ?, ?, ?, ?, ?)", "first pacifier ", -999, -15, 0, 9, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('12', ?, ?, ?, ?, ?, ?)", "Preparing my little room ", -999, -15, 1, 9, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('13', ?, ?, ?, ?, ?, ?)", "Buying stuff from my trousseau ", -999, -15, 1, 9, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('14', ?, ?, ?, ?, ?, ?)", "Kicking in Mommy's tummy ", -120, -1, 1, 9, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('15', ?, ?, ?, ?, ?, ?)", "Stretching myself in Mommy's tummy ", -120, -1, 1, 9, 15);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('16', ?, ?, ?, ?, ?, ?)", "Preparing my baby shower ", -90, -15, 0, 9, 16);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('17', ?, ?, ?, ?, ?, ?)", "My Baby Shower ", -90, -15, 0, 9, 17);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('18', ?, ?, ?, ?, ?, ?)", "A professional book for pregnant women ", -90, -15, 1, 9, 18);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('19', ?, ?, ?, ?, ?, ?)", "Going to maternity ", -15, 0, 0, 9, 19);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('20', ?, ?, ?, ?, ?, ?)", "Preparing my suitcase for maternity ", -15, 0, 0, 9, 20);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('21', ?, ?, ?, ?, ?, ?)", "Last photo in family before I was born ", -5, 0, 0, 9, 21);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('22', ?, ?, ?, ?, ?, ?)", "Mama I prepared for birth ", -5, 0, 1, 9, 22);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('23', ?, ?, ?, ?, ?, ?)", "Coming out of Mommy's tummy ", -5, 0, 0, 8, 23);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('24', ?, ?, ?, ?, ?, ?)", "Mom arriving at the hospital to greet me ", -5, 0, 0, 8, 24);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('25', ?, ?, ?, ?, ?, ?)", "My first bath ", -2, 2, 0, 4, 25);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('26', ?, ?, ?, ?, ?, ?)", "Seeing my weight ", -2, 2, 0, 8, 26);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('27', ?, ?, ?, ?, ?, ?)", "my umbiguinho ", -2, 2, 1, 8, 27);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('28', ?, ?, ?, ?, ?, ?)", "Mom picked me up ", 0, 2, 0, 6, 28);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('29', ?, ?, ?, ?, ?, ?)", "Dad picked me up ", 0, 2, 0, 6, 29);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('30', ?, ?, ?, ?, ?, ?)", "Grandma picked me up ", 0, 2, 0, 6, 30);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('31', ?, ?, ?, ?, ?, ?)", "Grandpa picked me up ", 0, 2, 0, 6, 31);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('32', ?, ?, ?, ?, ?, ?)", "Godmother picked me up ", 0, 2, 0, 6, 32);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('33', ?, ?, ?, ?, ?, ?)", "Godfather picked me up ", 0, 2, 0, 6, 33);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('34', ?, ?, ?, ?, ?, ?)", "Gave a handshake in fingers ", 0, 2, 0, 0, 34);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('35', ?, ?, ?, ?, ?, ?)", "I opened my little eyes ", 0, 2, 0, 0, 35);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('36', ?, ?, ?, ?, ?, ?)", "I changed diapers ", 0, 2, 0, 0, 36);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('37', ?, ?, ?, ?, ?, ?)", "When my family saw me ", 0, 2, 0, 1, 37);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('38', ?, ?, ?, ?, ?, ?)", "first feeding ", 0, 2, 0, 8, 38);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('39', ?, ?, ?, ?, ?, ?)", "Receiving visit the maternity ", 0, 5, 1, 1, 39);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('40', ?, ?, ?, ?, ?, ?)", "In the lap of Mother ", 0, 10, 1, 6, 40);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('41', ?, ?, ?, ?, ?, ?)", "On the lap of Santa ", 0, 10, 1, 6, 41);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('42', ?, ?, ?, ?, ?, ?)", "In Grandpa's lap ", 0, 10, 1, 6, 42);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('43', ?, ?, ?, ?, ?, ?)", "In the lap of Grandma ", 0, 10, 1, 6, 43);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('44', ?, ?, ?, ?, ?, ?)", "On lap Godmother ", 0, 10, 1, 6, 44);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('45', ?, ?, ?, ?, ?, ?)", "In the lap of the Godfather ", 0, 10, 1, 6, 45);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('46', ?, ?, ?, ?, ?, ?)", "One chorinho ... ", 0, 15, 1, 5, 39);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('47', ?, ?, ?, ?, ?, ?)", "Handshake of baby ... ", 0, 30, 1, 0, 40);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('48', ?, ?, ?, ?, ?, ?)", "Tomei injection ", 0, 30, 0, 0, 41);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('49', ?, ?, ?, ?, ?, ?)", "Yawning ... ", 0, 30, 1, 3, 42);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('50', ?, ?, ?, ?, ?, ?)", "Sleeping tasty ... ", 0, 30, 1, 3, 43);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('51', ?, ?, ?, ?, ?, ?)", "My first Smile ", 0, 30, 0, 5, 44);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('52', ?, ?, ?, ?, ?, ?)", "Cut nails ", 1, 30, 0, 0, 45);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('53', ?, ?, ?, ?, ?, ?)", "Cut nails ", 1, 30, 0, 3, 46);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('54', ?, ?, ?, ?, ?, ?)", "I bathing cat ", 2, 10, 1, 4, 47);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('55', ?, ?, ?, ?, ?, ?)", "Leaving the maternity ... going home! ", 2, 10, 0, 2, 48);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('56', ?, ?, ?, ?, ?, ?)", "Got home ", 2, 10, 0, 2, 49);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('57', ?, ?, ?, ?, ?, ?)", "I pulled a nap in my crib ", 2, 10, 0, 3, 50);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('58', ?, ?, ?, ?, ?, ?)", "I took a bath ", 2, 20, 0, 4, 51);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('59', ?, ?, ?, ?, ?, ?)", "I with 100 hours of life! ", 3, 5, 0, 7, 52);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('60', ?, ?, ?, ?, ?, ?)", "My first Ride ", 3, 30, 0, 2, 53);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('61', ?, ?, ?, ?, ?, ?)", "I visited my grandparents' house ", 3, 30, 0, 2, 54);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('62', ?, ?, ?, ?, ?, ?)", "When dropped my umbiguinho ", 7, 20, 0, 0, 55);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('63', ?, ?, ?, ?, ?, ?)", "Along with my first toy ", 5, 30, 1, 1, 56);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('64', ?, ?, ?, ?, ?, ?)", "Artistic nude! ", 5, 30, 1, 0, 57);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('65', ?, ?, ?, ?, ?, ?)", "my newborn screening ", 5, 30, 1, 0, 58);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('66', ?, ?, ?, ?, ?, ?)", "my little hand ", 5, 30, 1, 0, 59);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('67', ?, ?, ?, ?, ?, ?)", "my mouth ", 5, 30, 1, 0, 60);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('68', ?, ?, ?, ?, ?, ?)", "used pacifiers ", 5, 30, 0, 0, 61);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('69', ?, ?, ?, ?, ?, ?)", "Cutting the nails ", 5, 30, 1, 0, 62);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('70', ?, ?, ?, ?, ?, ?)", "Went on the pediatric ", 5, 30, 0, 0, 63);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('71', ?, ?, ?, ?, ?, ?)", "I showering ", 10, 30, 1, 4, 64);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('72', ?, ?, ?, ?, ?, ?)", "To crying, but soon passes ", 16, 30, 1, 5, 65);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('73', ?, ?, ?, ?, ?, ?)", "Getting home visit ", 2, 60, 1, 1, 66);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('74', ?, ?, ?, ?, ?, ?)", "Strolling in cart ", 5, 60, 1, 2, 67);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('75', ?, ?, ?, ?, ?, ?)", "sucking my pacifier ", 15, 45, 1, 0, 68);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('76', ?, ?, ?, ?, ?, ?)", "I walked in the square ", 15, 60, 0, 2, 69);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('77', ?, ?, ?, ?, ?, ?)", "I put my foot in mouth ", 20, 45, 0, 0, 70);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('78', ?, ?, ?, ?, ?, ?)", "I sucked my finger ", 20, 45, 0, 0, 71);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('79', ?, ?, ?, ?, ?, ?)", "I took a bottle ", 20, 45, 0, 0, 72);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('80', ?, ?, ?, ?, ?, ?)", "Eating my little foot ", 20, 45, 1, 0, 73);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('81', ?, ?, ?, ?, ?, ?)", "sucking fingers ", 20, 45, 1, 0, 74);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('82', ?, ?, ?, ?, ?, ?)", "Enjoying a song ", 20, 45, 1, 0, 75);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('83', ?, ?, ?, ?, ?, ?)", "My first monthly magazine ", 28, 32, 0, 7, 76);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('84', ?, ?, ?, ?, ?, ?)", "I with 1000 hours of life! ", 40, 43, 0, 7, 77);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('85', ?, ?, ?, ?, ?, ?)", "Changing diapers ", 0, 90, 1, 0, 78);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('86', ?, ?, ?, ?, ?, ?)", "Sucking happy life ", 0, 120, 1, 1, 78);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('87', ?, ?, ?, ?, ?, ?)", "Portraits you would most love ", 3, 90, 1, 1, 79);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('88', ?, ?, ?, ?, ?, ?)", "Moment Lion King ", 5, 90, 1, 1, 80);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('89', ?, ?, ?, ?, ?, ?)", "Any time. As my beautiful face! ", 5, 90, 1, 1, 81);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('90', ?, ?, ?, ?, ?, ?)", "at church ", 5, 90, 0, 2, 82);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('91', ?, ?, ?, ?, ?, ?)", "I went to a shopping ", 5, 90, 0, 2, 83);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('92', ?, ?, ?, ?, ?, ?)", "I was in a supermarket ", 5, 90, 0, 2, 84);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('93', ?, ?, ?, ?, ?, ?)", "Strolling car ", 5, 90, 1, 2, 85);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('94', ?, ?, ?, ?, ?, ?)", "Sleeping in cart ", 5, 90, 1, 3, 86);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('95', ?, ?, ?, ?, ?, ?)", "Nap in the car ", 5, 90, 1, 3, 87);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('96', ?, ?, ?, ?, ?, ?)", "My favorite team ", 10, 90, 1, 1, 88);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('97', ?, ?, ?, ?, ?, ?)", "Visit the pediatrician ", 15, 90, 1, 0, 89);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('98', ?, ?, ?, ?, ?, ?)", "Playing in bed ", 30, 90, 1, 1, 90);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('99', ?, ?, ?, ?, ?, ?)", "Laughing, smiling, having fun ", 45, 90, 1, 5, 91);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('100', ?, ?, ?, ?, ?, ?)", "My first laugh ", 45, 90, 0, 5, 92);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('101', ?, ?, ?, ?, ?, ?)", "According to my monthly magazine ", 58, 63, 0, 7, 93);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('102', ?, ?, ?, ?, ?, ?)", "Nap with Dad ", 1, 120, 1, 3, 94);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('103', ?, ?, ?, ?, ?, ?)", "Nap with Mom ", 1, 120, 1, 3, 95);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('104', ?, ?, ?, ?, ?, ?)", "Strolling like a kangaroo ", 15, 120, 1, 2, 96);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('105', ?, ?, ?, ?, ?, ?)", "I held an object ", 30, 120, 0, 0, 97);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('106', ?, ?, ?, ?, ?, ?)", "Tracking movements ", 30, 120, 1, 0, 98);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('107', ?, ?, ?, ?, ?, ?)", "Moments with friends ", 30, 120, 1, 7, 99);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('108', ?, ?, ?, ?, ?, ?)", "Moments with Dad ", 30, 120, 1, 1, 100);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('109', ?, ?, ?, ?, ?, ?)", "Moments with Mom ", 30, 120, 1, 1, 101);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('110', ?, ?, ?, ?, ?, ?)", "Moments with my parents ", 30, 120, 1, 1, 102);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('111', ?, ?, ?, ?, ?, ?)", "Moments with Grandpa ", 30, 120, 1, 1, 103);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('112', ?, ?, ?, ?, ?, ?)", "Moments with Grandma ", 30, 120, 1, 1, 104);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('113', ?, ?, ?, ?, ?, ?)", "Moments with my godparents ", 30, 120, 1, 1, 105);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('114', ?, ?, ?, ?, ?, ?)", "Moments in touch ", 30, 120, 1, 1, 106);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('115', ?, ?, ?, ?, ?, ?)", "Walking in my grandparents' house ", 30, 120, 1, 2, 107);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('116', ?, ?, ?, ?, ?, ?)", "I lifted her little head ", 60, 120, 0, 0, 108);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('117', ?, ?, ?, ?, ?, ?)", "movements followed ", 60, 120, 0, 0, 109);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('118', ?, ?, ?, ?, ?, ?)", "turned ", 60, 120, 0, 0, 110);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('119', ?, ?, ?, ?, ?, ?)", "Grimacing for new tidbits ", 60, 120, 1, 0, 111);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('120', ?, ?, ?, ?, ?, ?)", "I started laughing ", 60, 120, 0, 5, 112);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('121', ?, ?, ?, ?, ?, ?)", "My Third monthly publication ", 88, 92, 0, 7, 113);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('122', ?, ?, ?, ?, ?, ?)", "Showered Sea ", 15, 150, 0, 4, 114);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('123', ?, ?, ?, ?, ?, ?)", "Tomei sunbathing ", 15, 150, 0, 4, 115);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('124', ?, ?, ?, ?, ?, ?)", "My monthly publication Room ", 118, 123, 0, 7, 116);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('125', ?, ?, ?, ?, ?, ?)", "attending Mass ", 15, 180, 1, 2, 117);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('126', ?, ?, ?, ?, ?, ?)", "Strolling in the mall ", 15, 180, 1, 2, 118);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('127', ?, ?, ?, ?, ?, ?)", "Groceries ", 15, 180, 1, 2, 119);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('128', ?, ?, ?, ?, ?, ?)", "I went to see a movie at the cinema ", 30, 180, 0, 2, 120);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('129', ?, ?, ?, ?, ?, ?)", "Machucadinho ", 60, 180, 0, 0, 121);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('130', ?, ?, ?, ?, ?, ?)", "unable to sit ", 90, 180, 0, 0, 122);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('131', ?, ?, ?, ?, ?, ?)", "trying to sit ", 90, 180, 1, 0, 123);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('132', ?, ?, ?, ?, ?, ?)", "watching television ", 180, 720, 1, 1, 124);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('133', ?, ?, ?, ?, ?, ?)", "Trying to talk on the phone ", 360, 720, 1, 1, 125);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('134', ?, ?, ?, ?, ?, ?)", "Playing in the playground ", 360, 720, 1, 2, 126);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('135', ?, ?, ?, ?, ?, ?)", "Enjoying the little party ", 360, 720, 1, 2, 127);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('136', ?, ?, ?, ?, ?, ?)", "Falling asleep ", 360, 720, 1, 3, 128);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('137', ?, ?, ?, ?, ?, ?)", "At the restaurant ", 90, 180, 0, 2, 129);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('138', ?, ?, ?, ?, ?, ?)", "Enjoying the restaurant ", 180, 360, 1, 2, 130);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('139', ?, ?, ?, ?, ?, ?)", "Playing with my sister ", 180, 720, 1, 1, 131);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('140', ?, ?, ?, ?, ?, ?)", "Playing with my brother ", 180, 720, 1, 1, 132);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('141', ?, ?, ?, ?, ?, ?)", "Playing with buddies ", 180, 720, 1, 2, 133);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('142', ?, ?, ?, ?, ?, ?)", "Eating what should not ", 180, 720, 1, 0, 134);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('143', ?, ?, ?, ?, ?, ?)", "My fifth monthly publication ", 148, 153, 0, 7, 135);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('144', ?, ?, ?, ?, ?, ?)", "My sixth monthly publication ", 178, 182, 0, 7, 136);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('145', ?, ?, ?, ?, ?, ?)", "My seventh monthly publication ", 208, 212, 0, 7, 137);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('146', ?, ?, ?, ?, ?, ?)", "My eighth monthly publication ", 238, 242, 0, 7, 138);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('147', ?, ?, ?, ?, ?, ?)", "My new monthly magazine ", 268, 272, 0, 7, 139);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('148', ?, ?, ?, ?, ?, ?)", "My monthly magazine Tenth ", 298, 302, 0, 7, 140);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('149', ?, ?, ?, ?, ?, ?)", "taking vaccine ", 15, 360, 1, 0, 141);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('150', ?, ?, ?, ?, ?, ?)", "my baptism ", 30, 360, 0, 7, 142);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('151', ?, ?, ?, ?, ?, ?)", "Walk in the park ", 30, 360, 1, 2, 143);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('152', ?, ?, ?, ?, ?, ?)", "Went to the beach ", 30, 360, 0, 2, 144);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('153', ?, ?, ?, ?, ?, ?)", "Walking on the beach ", 30, 360, 1, 2, 145);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('154', ?, ?, ?, ?, ?, ?)", "Showered Pool ", 60, 360, 0, 4, 146);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('155', ?, ?, ?, ?, ?, ?)", "Pool bath ", 60, 360, 1, 4, 147);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('156', ?, ?, ?, ?, ?, ?)", "ate porridge ", 90, 360, 0, 0, 148);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('157', ?, ?, ?, ?, ?, ?)", "eating porridge ", 90, 360, 1, 0, 149);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('158', ?, ?, ?, ?, ?, ?)", "I was at the zoo ", 90, 360, 0, 2, 150);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('159', ?, ?, ?, ?, ?, ?)", "Playing on the playground ", 90, 360, 1, 2, 151);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('160', ?, ?, ?, ?, ?, ?)", "Just went out and tried to talk noise ", 120, 360, 0, 0, 152);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('161', ?, ?, ?, ?, ?, ?)", "I cute ", 120, 360, 0, 0, 153);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('162', ?, ?, ?, ?, ?, ?)", "I began to crawl ", 120, 360, 0, 0, 154);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('163', ?, ?, ?, ?, ?, ?)", "Speaking the language of babies ", 120, 360, 1, 0, 155);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('164', ?, ?, ?, ?, ?, ?)", "Crawling around the house ", 120, 720, 1, 0, 156);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('165', ?, ?, ?, ?, ?, ?)", "Crawling around the world ", 120, 720, 1, 0, 157);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('166', ?, ?, ?, ?, ?, ?)", "Began to rise to a tooth ", 150, 720, 0, 0, 158);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('167', ?, ?, ?, ?, ?, ?)", "I stood ", 150, 720, 0, 0, 159);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('168', ?, ?, ?, ?, ?, ?)", "teeth growing ", 150, 360, 1, 0, 160);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('169', ?, ?, ?, ?, ?, ?)", "trying to walk ", 150, 360, 1, 0, 161);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('170', ?, ?, ?, ?, ?, ?)", "Bati Palminha ", 180, 360, 0, 0, 162);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('171', ?, ?, ?, ?, ?, ?)", "hitting Palminha ", 180, 360, 1, 0, 163);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('172', ?, ?, ?, ?, ?, ?)", "Boo-boo but soon passes ", 180, 720, 1, 0, 164);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('173', ?, ?, ?, ?, ?, ?)", "I was in a circus ", 180, 360, 0, 2, 165);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('174', ?, ?, ?, ?, ?, ?)", "Full of sunscreen ", 180, 360, 1, 2, 166);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('175', ?, ?, ?, ?, ?, ?)", "My Eleventh monthly publication ", 328, 333, 0, 7, 167);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('176', ?, ?, ?, ?, ?, ?)", "My first birthday! ", 358, 362, 0, 7, 168);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('177', ?, ?, ?, ?, ?, ?)", "I made a trip ", 120, 420, 0, 7, 169);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('178', ?, ?, ?, ?, ?, ?)", "I spoke a word he gave to understand ", 120, 720, 0, 0, 170);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('179', ?, ?, ?, ?, ?, ?)", "I told Mom ", 120, 240, 0, 0, 171);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('180', ?, ?, ?, ?, ?, ?)", "I told Dad ", 120, 240, 0, 0, 172);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('181', ?, ?, ?, ?, ?, ?)", "I cut my hair ", 120, 500, 0, 0, 173);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('182', ?, ?, ?, ?, ?, ?)", "Cutting my hair ", 120, 240, 1, 0, 174);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('183', ?, ?, ?, ?, ?, ?)", "taking injection ", 90, 180, 1, 0, 175);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('184', ?, ?, ?, ?, ?, ?)", "Me and my babysitter ", 90, 180, 1, 10, 176);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('185', ?, ?, ?, ?, ?, ?)", "First day at daycare ", 180, 720, 0, 10, 177);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('186', ?, ?, ?, ?, ?, ?)", "Preparing for daycare ", 720, 720, 1, 10, 178);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('187', ?, ?, ?, ?, ?, ?)", "Small party at daycare ", 180, 720, 1, 10, 179);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('188', ?, ?, ?, ?, ?, ?)", "Mom visiting me at daycare ", 180, 720, 1, 10, 180);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('189', ?, ?, ?, ?, ?, ?)", "Strolling at the zoo ", 180, 720, 1, 2, 181);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('190', ?, ?, ?, ?, ?, ?)", "I was in a museum ", 360, 720, 0, 2, 182);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('191', ?, ?, ?, ?, ?, ?)", "I was in theater ", 360, 720, 0, 2, 183);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('192', ?, ?, ?, ?, ?, ?)", "Watching movie in cinema ", 360, 720, 1, 2, 184);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('193', ?, ?, ?, ?, ?, ?)", "Strolling in the circus ", 360, 720, 1, 2, 185);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('194', ?, ?, ?, ?, ?, ?)", "My first Christmas ", 999, 999, 0, 7, 186);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('195', ?, ?, ?, ?, ?, ?)", "My first mothers day ", 999, 999, 0, 7, 187);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('196', ?, ?, ?, ?, ?, ?)", "Mothers Day! ", 999, 999, 1, 7, 188);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('197', ?, ?, ?, ?, ?, ?)", "My first fathers day ", 999, 999, 0, 7, 189);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('198', ?, ?, ?, ?, ?, ?)", "Fathers Day! ", 999, 999, 1, 7, 190);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('199', ?, ?, ?, ?, ?, ?)", "Walked plane ", 999, 999, 0, 2, 188);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('200', ?, ?, ?, ?, ?, ?)", "First day of kindergarten ", 720, 1440, 0, 10, 189);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('201', ?, ?, ?, ?, ?, ?)", "my backpack ", 720, 1440, 0, 10, 190);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('202', ?, ?, ?, ?, ?, ?)", "Getting ready for kindergarten ", 720, 1440, 1, 10, 191);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('203', ?, ?, ?, ?, ?, ?)", "Small party at kindergarten ", 720, 1440, 1, 10, 192);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('204', ?, ?, ?, ?, ?, ?)", "Watching a theater ", 1440, 2880, 1, 2, 193);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('205', ?, ?, ?, ?, ?, ?)", "tooth mole ", 1620, 3240, 0, 0, 194);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('206', ?, ?, ?, ?, ?, ?)", "My tooth fell ", 1620, 3240, 0, 0, 195);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('207', ?, ?, ?, ?, ?, ?)", "Over a tooth falling ", 1620, 3240, 1, 0, 196);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('208', ?, ?, ?, ?, ?, ?)", "Doing handiwork school ", 720, 1440, 1, 10, 197);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('209', ?, ?, ?, ?, ?, ?)", "Feast of 1 aninho ", 345, 390, 0, 7, 198);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('210', ?, ?, ?, ?, ?, ?)", "Party of 2 year olds ", 690, 780, 0, 7, 199);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('211', ?, ?, ?, ?, ?, ?)", "Party of 3 year olds ", 1380, 1560, 0, 7, 200);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('212', ?, ?, ?, ?, ?, ?)", "Party of 4 year olds ", 2760, 3120, 0, 7, 201);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('213', ?, ?, ?, ?, ?, ?)", "Festinha birthday", 345, 3120, 1, 7, 202);
			}
		}
	}
}