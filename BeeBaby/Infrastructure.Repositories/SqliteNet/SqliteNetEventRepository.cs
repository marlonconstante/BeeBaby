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
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('1', ?, ?, ?, ?, ?)", "Mamãe chegando no hospital para me receber", 0, 0, 0, 5);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('2', ?, ?, ?, ?, ?)", "Eu nascendo...", 0, 0, 0, 5);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('3', ?, ?, ?, ?, ?)", "Minhã mãe me pegou no colo", 0, 2, 0, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('4', ?, ?, ?, ?, ?)", "Mamei", 0, 2, 0, 3);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('5', ?, ?, ?, ?, ?)", "Papai me pegou no colo", 0, 2, 0, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('6', ?, ?, ?, ?, ?)", "a Vovó me pegou no colo", 0, 2, 0, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('7', ?, ?, ?, ?, ?)", "o Vovô me pegou no colo", 0, 2, 0, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('8', ?, ?, ?, ?, ?)", "minha familia me viu", 0, 2, 0, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('9', ?, ?, ?, ?, ?)", "a Dinda me pegou no colo", 0, 2, 0, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('10', ?, ?, ?, ?, ?)", "o Dindo me pegou no colo", 0, 2, 0, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('11', ?, ?, ?, ?, ?)", "fui pesado", 0, 2, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('12', ?, ?, ?, ?, ?)", "Troquei de Fraldas", 0, 2, 0, 2);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('13', ?, ?, ?, ?, ?)", "Abri meus Olhinhos", 0, 2, 0, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('14', ?, ?, ?, ?, ?)", "dei um aperto de mão nos dedinhos", 0, 2, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('15', ?, ?, ?, ?, ?)", "tomei banho", 0, 2, 0, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('16', ?, ?, ?, ?, ?)", "Eu tomando banho de gato", 0, 10, 1, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('17', ?, ?, ?, ?, ?)", "Eu tomando banho", 0, 30, 1, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('18', ?, ?, ?, ?, ?)", "sorri", 0, 2, 0, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('19', ?, ?, ?, ?, ?)", "Só um chorinho...", 0, 15, 1, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('20', ?, ?, ?, ?, ?)", "To chorando, mas logo passa", 0, 15, 1, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('21', ?, ?, ?, ?, ?)", "da Dra que me fez nascer", 0, 5, 1, 5);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('22', ?, ?, ?, ?, ?)", "da Mamãe", 0, 10, 1, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('23', ?, ?, ?, ?, ?)", "do Papai", 0, 10, 1, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('24', ?, ?, ?, ?, ?)", "do Vovô", 0, 10, 1, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('25', ?, ?, ?, ?, ?)", "da Vovó", 0, 10, 1, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('26', ?, ?, ?, ?, ?)", "da Dinda", 0, 10, 1, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('27', ?, ?, ?, ?, ?)", "do Dindo", 0, 10, 1, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('28', ?, ?, ?, ?, ?)", "Nú artistico!", 0, 30, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('29', ?, ?, ?, ?, ?)", "Trocando as fraldas", 0, 90, 1, 2);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('30', ?, ?, ?, ?, ?)", "Meu pezinho", 0, 30, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('31', ?, ?, ?, ?, ?)", "Minha mãozinha", 0, 30, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('32', ?, ?, ?, ?, ?)", "Minha boquinha", 0, 30, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('33', ?, ?, ?, ?, ?)", "Recebendo visita na maternidade", 0, 5, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('34', ?, ?, ?, ?, ?)", "fui passear", 0, 30, 0, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('35', ?, ?, ?, ?, ?)", "Passeando no carrinho", 0, 60, 1, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('36', ?, ?, ?, ?, ?)", "Dormindo no carrinho", 0, 90, 1, 4);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('37', ?, ?, ?, ?, ?)", "Passeando de carro", 0, 90, 1, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('38', ?, ?, ?, ?, ?)", "Soninho no carro", 0, 90, 1, 4);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('39', ?, ?, ?, ?, ?)", "dei uma gargalhada", 0, 60, 0, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('40', ?, ?, ?, ?, ?)", "fiz uma visita na casa dos meu avós", 0, 30, 0, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('41', ?, ?, ?, ?, ?)", "tomei mamadeira", 0, 30, 0, 3);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('42', ?, ?, ?, ?, ?)", "coloquei meu pé na boca", 0, 30, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('43', ?, ?, ?, ?, ?)", "chupei meu dedo", 0, 30, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('44', ?, ?, ?, ?, ?)", "Saindo da maternidade... indo pra casa!", 0, 10, 0, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('45', ?, ?, ?, ?, ?)", "Cheguei em casa", 0, 10, 0, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('46', ?, ?, ?, ?, ?)", "Tirei um soninho no meu berço", 0, 10, 0, 6);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('47', ?, ?, ?, ?, ?)", "Tomei banho de banheira", 0, 20, 0, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('48', ?, ?, ?, ?, ?)", "Recebendo visita em casa", 0, 60, 1, 0);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('49', ?, ?, ?, ?, ?)", "Bocejando...", 0, 20, 1, 6);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('50', ?, ?, ?, ?, ?)", "Dormindo gostoso...", 0, 30, 1, 6);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('51', ?, ?, ?, ?, ?)", "Aperto de mão de bebê...", 0, 30, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('52', ?, ?, ?, ?, ?)", "Meu time do coração", 0, 90, 1, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('53', ?, ?, ?, ?, ?)", "Brincando na cama", 0, 90, 1, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('54', ?, ?, ?, ?, ?)", "Rindo, sorrindo, me divertindo", 0, 90, 1, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('55', ?, ?, ?, ?, ?)", "Retratos com quem mais amo", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('56', ?, ?, ?, ?, ?)", "Passei um Natal", 0, 10000, 0, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('57', ?, ?, ?, ?, ?)", "Fazendo careta para comidinhas novas", 0, 90, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('58', ?, ?, ?, ?, ?)", "Junto com meu primeiro brinquedinho", 0, 20, 1, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('59', ?, ?, ?, ?, ?)", "Nenhum momento. Apenas minha cara linda!", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('60', ?, ?, ?, ?, ?)", "Fui para praia", 0, 60, 0, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('61', ?, ?, ?, ?, ?)", "Na praia", 0, 60, 1, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('62', ?, ?, ?, ?, ?)", "No parque", 0, 60, 1, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('63', ?, ?, ?, ?, ?)", "Na casa dos meus avós", 0, 90, 1, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('64', ?, ?, ?, ?, ?)", "papai...", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('65', ?, ?, ?, ?, ?)", "mamãe...", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('66', ?, ?, ?, ?, ?)", "meus pais...", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('67', ?, ?, ?, ?, ?)", "meu Vovô", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('68', ?, ?, ?, ?, ?)", "minha Vovó", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('69', ?, ?, ?, ?, ?)", "minha Dinda", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('70', ?, ?, ?, ?, ?)", "meu Dindo", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('71', ?, ?, ?, ?, ?)", "Momentos com amigos...", 0, 90, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('72', ?, ?, ?, ?, ?)", "Momentos em familia...", 0, 90, 1, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('73', ?, ?, ?, ?, ?)", "Eu com 100 horas de vida!", 0, 5, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('74', ?, ?, ?, ?, ?)", "Eu com 1000 horas de vida!", 0, 43, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('75', ?, ?, ?, ?, ?)", "Meu Primeiro mensário", 0, 32, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('76', ?, ?, ?, ?, ?)", "Meu Segundo mensário", 0, 63, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('77', ?, ?, ?, ?, ?)", "Meu Terceiro mensário", 0, 92, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('78', ?, ?, ?, ?, ?)", "Meu Quarto mensário", 0, 123, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('79', ?, ?, ?, ?, ?)", "Meu Quinto mensário", 0, 153, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('80', ?, ?, ?, ?, ?)", "Meu Sexto mensário", 0, 182, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('81', ?, ?, ?, ?, ?)", "Meu Sétimo mensário", 0, 212, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('82', ?, ?, ?, ?, ?)", "Meu Oitavo mensário", 0, 242, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('83', ?, ?, ?, ?, ?)", "Meu Novo mensário", 0, 272, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('84', ?, ?, ?, ?, ?)", "Meu Décimo mensário", 0, 302, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('85', ?, ?, ?, ?, ?)", "Meu Décimo Primeiro mensário", 0, 333, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('86', ?, ?, ?, ?, ?)", "Meu Primeiro Aninho!", 0, 362, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('87', ?, ?, ?, ?, ?)", "Cortei as unhas", 0, 30, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('88', ?, ?, ?, ?, ?)", "Cortando as unhas", 0, 30, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('89', ?, ?, ?, ?, ?)", "Meu batizado", 0, 360, 0, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('90', ?, ?, ?, ?, ?)", "Levantei a cabecinha", 0, 120, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('91', ?, ?, ?, ?, ?)", "Acompanhei movimentos", 0, 120, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('92', ?, ?, ?, ?, ?)", "Acompanhando movimentos", 0, 120, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('93', ?, ?, ?, ?, ?)", "Me virei", 0, 120, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('94', ?, ?, ?, ?, ?)", "Segurei um objeto", 0, 120, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('95', ?, ?, ?, ?, ?)", "tentei falar e só saiu barulho", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('96', ?, ?, ?, ?, ?)", "Falando a lingua de bebês", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('97', ?, ?, ?, ?, ?)", "falei uma palavra que dava pra entender", 0, 500, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('98', ?, ?, ?, ?, ?)", "falei mamãe", 0, 500, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('99', ?, ?, ?, ?, ?)", "falei papai", 0, 500, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('100', ?, ?, ?, ?, ?)", "comi papinha", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('101', ?, ?, ?, ?, ?)", "Comendo papinha", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('102', ?, ?, ?, ?, ?)", "fiz gracinha", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('103', ?, ?, ?, ?, ?)", "Comecei a engatinhar", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('104', ?, ?, ?, ?, ?)", "Engatinhando pela casa", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('105', ?, ?, ?, ?, ?)", "Engatinhando pelo mundo", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('106', ?, ?, ?, ?, ?)", "Começou a nascer um dentinho", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('107', ?, ?, ?, ?, ?)", "Dentinhos crescendo", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('108', ?, ?, ?, ?, ?)", "Cortei os cabelos", 0, 500, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('109', ?, ?, ?, ?, ?)", "Cortando meus cabelos", 0, 500, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('110', ?, ?, ?, ?, ?)", "Fiquei de pé", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('111', ?, ?, ?, ?, ?)", "Tentando caminhar", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('112', ?, ?, ?, ?, ?)", "Tomei banho de sol", 0, 150, 0, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('113', ?, ?, ?, ?, ?)", "Tomei banho de mar", 0, 150, 0, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('114', ?, ?, ?, ?, ?)", "Consegui sentar", 0, 180, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('115', ?, ?, ?, ?, ?)", "Tentando sentar", 0, 180, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('116', ?, ?, ?, ?, ?)", "Bati palminha", 0, 360, 0, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('117', ?, ?, ?, ?, ?)", "Batendo palminha", 0, 360, 1, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('118', ?, ?, ?, ?, ?)", "Meu primeiro dia das mães", 0, 10000, 1, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('119', ?, ?, ?, ?, ?)", "fiz uma viagem", 0, 10000, 0, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('120', ?, ?, ?, ?, ?)", "Momento Rei Leão", 0, 10000, 1, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('121', ?, ?, ?, ?, ?)", "tomei banho de piscina", 0, 30, 0, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag) values ('122', ?, ?, ?, ?, ?)", "Na psicina", 0, 30, 1, 9);
			}
		}
	}
}

