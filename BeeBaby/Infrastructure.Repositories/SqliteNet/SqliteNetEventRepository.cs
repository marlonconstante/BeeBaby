using System;
using Domain.Moment;
using SQLite.Net;
using Infrastructure.Repositories.SqliteNet.Entities;
using Infrastructure.Repositories.SqliteNet.Mapper;
using Skahal.Infrastructure.Framework.Repositories;
using System.Text;
using System.Collections.Generic;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories.SqliteNet
{
	public class SqliteNetEventRepository: SqliteNetRepositoryBase<Event, EventData>, IEventRepository
	{
		public SqliteNetEventRepository(SQLiteConnection connection, IUnitOfWork unitOfWork) : base(connection, new SqliteNetEventMapper(), unitOfWork)
		{
			#region CreateData
			connection.CreateTable<EventData>();

			if (CountAll(null) <= 0)
			{
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('1', ?, ?, ?, ?, ?, ?)", "Na barriga da mamãe", -999, 0, 1, 9, 1);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('2', ?, ?, ?, ?, ?, ?)", "Preparando meu chá de fraldas", -999, 0, 0, 9, 2);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('3', ?, ?, ?, ?, ?, ?)", "Meu chá de fraldas", -999, 0, 0, 9, 3);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('4', ?, ?, ?, ?, ?, ?)", "Dia das mães na barriga", -999, 0, 0, 9, 4);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('5', ?, ?, ?, ?, ?, ?)", "Familia reunida em volta da barriga", -999, 0, 1, 9, 5);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('6', ?, ?, ?, ?, ?, ?)", "Ganhando beijo na barriga", -999, 0, 1, 9, 6);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('7', ?, ?, ?, ?, ?, ?)", "Dando chutes na barriga da mamãe", -999, 0, 1, 9, 7);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('8', ?, ?, ?, ?, ?, ?)", "Me espreguiçando na barriga da mamãe", -999, 0, 1, 9, 8);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('9', ?, ?, ?, ?, ?, ?)", "Me escutando na barriga da mamãe", -999, 0, 1, 9, 9);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('10', ?, ?, ?, ?, ?, ?)", "Mamãe fazendo exercício comigo na barriga", -999, 0, 1, 9, 10);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('11', ?, ?, ?, ?, ?, ?)", "Lendo revistas de bebês", -999, 0, 1, 9, 11);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('12', ?, ?, ?, ?, ?, ?)", "Um book profissional de gestante", -999, 0, 1, 9, 12);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('13', ?, ?, ?, ?, ?, ?)", "Indo para maternidade", -999, 0, 0, 9, 13);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('14', ?, ?, ?, ?, ?, ?)", "Preparando minha mala para maternidade", -999, 0, 0, 9, 14);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('15', ?, ?, ?, ?, ?, ?)", "Ultima foto em familia antes de eu nascer", -999, 0, 0, 9, 15);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('16', ?, ?, ?, ?, ?, ?)", "Mamãe preparada para eu nascer", -999, 0, 1, 9, 16);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('17', ?, ?, ?, ?, ?, ?)", "Meu primeiro sapatinho", -999, 0, 0, 9, 17);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('18', ?, ?, ?, ?, ?, ?)", "Minha primeira roupinha", -999, 0, 0, 9, 18);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('19', ?, ?, ?, ?, ?, ?)", "Meu primeiro brinquedinho", -999, 0, 0, 9, 19);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('20', ?, ?, ?, ?, ?, ?)", "Minha primeira chupeta", -999, 0, 0, 9, 20);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('21', ?, ?, ?, ?, ?, ?)", "Preparando meu quartinho", -999, 0, 1, 9, 21);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('22', ?, ?, ?, ?, ?, ?)", "Comprando coisas do meu enxoval", -999, 0, 1, 9, 22);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('23', ?, ?, ?, ?, ?, ?)", "Saindo da barriga da mamãe", -999, 0, 0, 8, 23);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('24', ?, ?, ?, ?, ?, ?)", "Mamãe chegando no hospital para me receber", -999, 0, 0, 8, 24);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('25', ?, ?, ?, ?, ?, ?)", "Vendo meu peso", -999, 0, 0, 8, 25);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('26', ?, ?, ?, ?, ?, ?)", "Primeira mamada", 0, 2, 0, 8, 26);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('27', ?, ?, ?, ?, ?, ?)", "Mamãe me pegou no colo", 0, 2, 0, 6, 27);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('28', ?, ?, ?, ?, ?, ?)", "Papai me pegou no colo", 0, 2, 0, 6, 28);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('29', ?, ?, ?, ?, ?, ?)", "Vovó me pegou no colo", 0, 2, 0, 6, 29);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('30', ?, ?, ?, ?, ?, ?)", "Vovô me pegou no colo", 0, 2, 0, 6, 30);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('31', ?, ?, ?, ?, ?, ?)", "Madrinha me pegou no colo", 0, 2, 0, 6, 31);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('32', ?, ?, ?, ?, ?, ?)", "Padrinho  me pegou no colo", 0, 2, 0, 6, 32);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('33', ?, ?, ?, ?, ?, ?)", "No colo da Mamãe", 0, 10, 1, 6, 33);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('34', ?, ?, ?, ?, ?, ?)", "No colo do Papai", 0, 10, 1, 6, 34);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('35', ?, ?, ?, ?, ?, ?)", "No colo do Vovô", 0, 10, 1, 6, 35);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('36', ?, ?, ?, ?, ?, ?)", "No colo da Vovó", 0, 10, 1, 6, 36);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('37', ?, ?, ?, ?, ?, ?)", "No colo da Madrinha", 0, 10, 1, 6, 37);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('38', ?, ?, ?, ?, ?, ?)", "No colo do Padrinho", 0, 10, 1, 6, 38);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('39', ?, ?, ?, ?, ?, ?)", "Aperto de mão de bebê...", 0, 30, 1, 0, 39);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('40', ?, ?, ?, ?, ?, ?)", "Bocejando...", 0, 30, 1, 3, 40);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('41', ?, ?, ?, ?, ?, ?)", "Dormindo gostoso...", 0, 30, 1, 3, 41);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('42', ?, ?, ?, ?, ?, ?)", "Meu 1o Banho", 0, 2, 0, 4, 42);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('43', ?, ?, ?, ?, ?, ?)", "Dei um aperto de mão nos dedinhos", 0, 2, 0, 0, 43);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('44', ?, ?, ?, ?, ?, ?)", "Abri meus Olhinhos", 0, 2, 0, 0, 44);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('45', ?, ?, ?, ?, ?, ?)", "Troquei de Fraldas", 0, 2, 0, 0, 45);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('46', ?, ?, ?, ?, ?, ?)", "Trocando as fraldas", 0, 90, 1, 0, 46);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('47', ?, ?, ?, ?, ?, ?)", "Meu 1o Sorriso", 0, 30, 0, 5, 47);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('48', ?, ?, ?, ?, ?, ?)", "Recebendo visita na maternidade", 0, 5, 1, 1, 48);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('49', ?, ?, ?, ?, ?, ?)", "Meu umbiguinho", 0, 0, 1, 8, 49);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('50', ?, ?, ?, ?, ?, ?)", "Só um chorinho...", 0, 15, 1, 5, 50);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('51', ?, ?, ?, ?, ?, ?)", "Quando minha familia me viu", 0, 2, 0, 1, 51);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('52', ?, ?, ?, ?, ?, ?)", "Tomei injeção", 0, 30, 0, 0, 52);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('53', ?, ?, ?, ?, ?, ?)", "A 1a noite acordando todo mundo", 1, 30, 0, 3, 53);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('54', ?, ?, ?, ?, ?, ?)", "Cortei as unhas", 1, 30, 0, 0, 54);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('55', ?, ?, ?, ?, ?, ?)", "Soneca com papai", 1, 120, 1, 3, 55);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('56', ?, ?, ?, ?, ?, ?)", "Soneca com mamãe", 1, 120, 1, 3, 56);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('57', ?, ?, ?, ?, ?, ?)", "Tomei banho de banheira", 2, 20, 0, 4, 57);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('58', ?, ?, ?, ?, ?, ?)", "Tirei um soninho no meu berço", 2, 10, 0, 3, 58);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('59', ?, ?, ?, ?, ?, ?)", "Eu tomando banho de gato", 2, 10, 1, 4, 59);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('60', ?, ?, ?, ?, ?, ?)", "Recebendo visita em casa", 2, 60, 1, 1, 60);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('61', ?, ?, ?, ?, ?, ?)", "Saindo da maternidade... indo pra casa!", 2, 10, 0, 2, 61);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('62', ?, ?, ?, ?, ?, ?)", "Cheguei em casa", 2, 10, 0, 2, 62);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('63', ?, ?, ?, ?, ?, ?)", "Meu 1o Passeio", 3, 30, 0, 2, 63);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('64', ?, ?, ?, ?, ?, ?)", "visitei a casa dos meus avós", 3, 30, 0, 2, 64);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('65', ?, ?, ?, ?, ?, ?)", "Retratos com quem mais amo", 3, 90, 1, 1, 65);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('66', ?, ?, ?, ?, ?, ?)", "Eu com 100 horas de vida!", 3, 5, 0, 7, 66);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('67', ?, ?, ?, ?, ?, ?)", "Nú artistico!", 5, 30, 1, 0, 67);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('68', ?, ?, ?, ?, ?, ?)", "Meu pezinho", 5, 30, 1, 0, 68);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('69', ?, ?, ?, ?, ?, ?)", "Minha mãozinha", 5, 30, 1, 0, 69);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('70', ?, ?, ?, ?, ?, ?)", "Minha boquinha", 5, 30, 1, 0, 70);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('71', ?, ?, ?, ?, ?, ?)", "Usei chupeta", 5, 30, 0, 0, 71);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('72', ?, ?, ?, ?, ?, ?)", "chupando minha chupeta", 15, 45, 1, 0, 72);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('73', ?, ?, ?, ?, ?, ?)", "Junto com meu primeiro brinquedinho", 5, 30, 1, 1, 73);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('74', ?, ?, ?, ?, ?, ?)", "Cortando as unhas", 5, 30, 1, 0, 74);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('75', ?, ?, ?, ?, ?, ?)", "Nenhum momento. Apenas minha cara linda!", 5, 90, 1, 1, 75);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('76', ?, ?, ?, ?, ?, ?)", "na igreja", 5, 90, 0, 2, 76);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('77', ?, ?, ?, ?, ?, ?)", "Fui no pediatra", 5, 30, 0, 0, 77);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('78', ?, ?, ?, ?, ?, ?)", "Momento Rei Leão", 5, 90, 1, 1, 78);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('79', ?, ?, ?, ?, ?, ?)", "Quando caiu meu umbiguinho", 5, 20, 0, 0, 79);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('80', ?, ?, ?, ?, ?, ?)", "Fui num shopping", 5, 90, 0, 2, 80);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('81', ?, ?, ?, ?, ?, ?)", "Fui num supermercado", 5, 90, 0, 2, 81);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('82', ?, ?, ?, ?, ?, ?)", "Passeando no carrinho", 5, 60, 1, 2, 82);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('83', ?, ?, ?, ?, ?, ?)", "Passeando de carro", 5, 90, 1, 2, 83);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('84', ?, ?, ?, ?, ?, ?)", "Dormindo no carrinho", 5, 90, 1, 3, 84);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('85', ?, ?, ?, ?, ?, ?)", "Soninho no carro", 5, 90, 1, 3, 85);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('86', ?, ?, ?, ?, ?, ?)", "coloquei meu pé na boca", 20, 45, 0, 0, 86);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('87', ?, ?, ?, ?, ?, ?)", "chupei meu dedo", 20, 45, 0, 0, 87);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('88', ?, ?, ?, ?, ?, ?)", "tomei mamadeira", 20, 45, 0, 1, 88);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('89', ?, ?, ?, ?, ?, ?)", "Eu tomando banho", 10, 30, 1, 4, 89);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('90', ?, ?, ?, ?, ?, ?)", "Meu time do coração", 10, 90, 1, 1, 90);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('91', ?, ?, ?, ?, ?, ?)", "Comendo meu pezinho", 20, 45, 1, 0, 91);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('92', ?, ?, ?, ?, ?, ?)", "chupando os dedinhos", 20, 45, 1, 0, 92);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('93', ?, ?, ?, ?, ?, ?)", "Curtindo uma música", 20, 45, 1, 0, 93);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('94', ?, ?, ?, ?, ?, ?)", "assistindo a missa", 15, 180, 1, 2, 93);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('95', ?, ?, ?, ?, ?, ?)", "Visita no pediatra", 15, 90, 1, 0, 94);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('96', ?, ?, ?, ?, ?, ?)", "Fui passear na pracinha", 15, 60, 0, 2, 95);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('97', ?, ?, ?, ?, ?, ?)", "Tomei banho de mar", 15, 150, 0, 4, 96);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('98', ?, ?, ?, ?, ?, ?)", "Tomei banho de sol", 15, 150, 0, 4, 97);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('99', ?, ?, ?, ?, ?, ?)", "Passeando como um canguru", 15, 120, 1, 2, 98);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('100', ?, ?, ?, ?, ?, ?)", "Passeando no shopping", 15, 180, 1, 2, 99);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('101', ?, ?, ?, ?, ?, ?)", "Compras no supermercado", 15, 180, 1, 2, 100);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('102', ?, ?, ?, ?, ?, ?)", "Tomando vacina", 15, 360, 1, 0, 101);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('103', ?, ?, ?, ?, ?, ?)", "To chorando, mas logo passa", 16, 30, 1, 5, 102);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('104', ?, ?, ?, ?, ?, ?)", "Meu Primeiro mensário", 28, 32, 0, 7, 103);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('105', ?, ?, ?, ?, ?, ?)", "Brincando na cama", 30, 90, 1, 1, 104);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('106', ?, ?, ?, ?, ?, ?)", "Rindo, sorrindo, me divertindo", 45, 90, 1, 5, 105);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('107', ?, ?, ?, ?, ?, ?)", "Minha 1a risada", 45, 90, 0, 5, 106);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('108', ?, ?, ?, ?, ?, ?)", "Levantei a cabecinha", 60, 120, 0, 0, 106);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('109', ?, ?, ?, ?, ?, ?)", "Acompanhei movimentos", 60, 120, 0, 0, 107);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('110', ?, ?, ?, ?, ?, ?)", "Me virei", 60, 120, 0, 0, 108);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('111', ?, ?, ?, ?, ?, ?)", "Segurei um objeto", 30, 120, 0, 0, 109);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('112', ?, ?, ?, ?, ?, ?)", "Acompanhando movimentos", 30, 120, 1, 0, 110);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('113', ?, ?, ?, ?, ?, ?)", "Meu batizado", 30, 360, 0, 7, 111);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('114', ?, ?, ?, ?, ?, ?)", "Momentos com amigos", 30, 120, 1, 7, 112);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('115', ?, ?, ?, ?, ?, ?)", "Momentos com papai", 30, 120, 1, 1, 113);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('116', ?, ?, ?, ?, ?, ?)", "Momentos com mamãe", 30, 120, 1, 1, 114);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('117', ?, ?, ?, ?, ?, ?)", "Momentos com meus pais", 30, 120, 1, 1, 115);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('118', ?, ?, ?, ?, ?, ?)", "Momentos com vovô", 30, 120, 1, 1, 116);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('119', ?, ?, ?, ?, ?, ?)", "Momentos com vovó", 30, 120, 1, 1, 117);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('120', ?, ?, ?, ?, ?, ?)", "Momentos com meus padrinhos", 30, 120, 1, 1, 118);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('121', ?, ?, ?, ?, ?, ?)", "Momentos em familia", 30, 120, 1, 1, 119);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('122', ?, ?, ?, ?, ?, ?)", "Fui ver um filme no cinema", 30, 180, 0, 2, 120);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('123', ?, ?, ?, ?, ?, ?)", "Passeio no parque", 30, 360, 1, 2, 121);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('124', ?, ?, ?, ?, ?, ?)", "Fui para praia", 30, 360, 0, 2, 122);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('125', ?, ?, ?, ?, ?, ?)", "Passeio na praia", 30, 360, 1, 2, 123);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('126', ?, ?, ?, ?, ?, ?)", "Passeio na casa dos meus avós", 30, 120, 1, 2, 124);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('127', ?, ?, ?, ?, ?, ?)", "Eu com 1000 horas de vida!", 40, 43, 0, 7, 125);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('128', ?, ?, ?, ?, ?, ?)", "Meu Segundo mensário", 58, 63, 0, 7, 126);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('129', ?, ?, ?, ?, ?, ?)", "Comecei a gargalhar", 60, 120, 0, 5, 127);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('130', ?, ?, ?, ?, ?, ?)", "Fazendo careta para comidinhas novas", 60, 120, 1, 0, 128);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('131', ?, ?, ?, ?, ?, ?)", "Machucadinho", 60, 180, 0, 0, 129);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('132', ?, ?, ?, ?, ?, ?)", "Tomei banho de piscina", 60, 360, 0, 4, 130);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('133', ?, ?, ?, ?, ?, ?)", "Banho de piscina", 60, 360, 1, 4, 131);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('134', ?, ?, ?, ?, ?, ?)", "Meu Terceiro mensário", 88, 92, 0, 7, 132);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('135', ?, ?, ?, ?, ?, ?)", "Consegui sentar", 90, 180, 0, 0, 133);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('136', ?, ?, ?, ?, ?, ?)", "Tentando sentar", 90, 180, 1, 0, 134);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('137', ?, ?, ?, ?, ?, ?)", "comi papinha", 90, 360, 0, 0, 135);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('138', ?, ?, ?, ?, ?, ?)", "Comendo papinha", 90, 360, 1, 0, 136);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('139', ?, ?, ?, ?, ?, ?)", "Eu e minha babá", 90, 720, 1, 10, 137);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('140', ?, ?, ?, ?, ?, ?)", "Fui no zoológico", 90, 360, 0, 2, 138);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('141', ?, ?, ?, ?, ?, ?)", "Brincando na pracinha", 90, 360, 1, 2, 139);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('142', ?, ?, ?, ?, ?, ?)", "Tomando injeção", 90, 720, 1, 0, 140);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('143', ?, ?, ?, ?, ?, ?)", "Meu Quarto mensário", 118, 123, 0, 7, 141);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('144', ?, ?, ?, ?, ?, ?)", "Tentei falar e só saiu barulho", 120, 360, 0, 0, 142);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('145', ?, ?, ?, ?, ?, ?)", "Falei uma palavra que dava pra entender", 120, 500, 0, 0, 143);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('146', ?, ?, ?, ?, ?, ?)", "falei mamãe", 120, 500, 0, 0, 144);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('147', ?, ?, ?, ?, ?, ?)", "falei papai", 120, 500, 0, 0, 145);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('148', ?, ?, ?, ?, ?, ?)", "fiz gracinha", 120, 360, 0, 0, 146);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('149', ?, ?, ?, ?, ?, ?)", "Comecei a engatinhar", 120, 360, 0, 0, 147);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('150', ?, ?, ?, ?, ?, ?)", "Falando a lingua de bebês", 120, 360, 1, 0, 148);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('151', ?, ?, ?, ?, ?, ?)", "Engatinhando pela casa", 120, 360, 1, 0, 149);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('152', ?, ?, ?, ?, ?, ?)", "Engatinhando pelo mundo", 120, 360, 1, 0, 150);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('153', ?, ?, ?, ?, ?, ?)", "Cortei os cabelos", 120, 500, 0, 0, 151);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('154', ?, ?, ?, ?, ?, ?)", "Cortando meus cabelos", 120, 500, 1, 0, 152);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('155', ?, ?, ?, ?, ?, ?)", "fiz uma viagem", 120, 420, 0, 7, 153);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('156', ?, ?, ?, ?, ?, ?)", "Meu Quinto mensário", 148, 153, 0, 7, 154);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('157', ?, ?, ?, ?, ?, ?)", "Começou a nascer um dentinho", 150, 360, 0, 0, 155);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('158', ?, ?, ?, ?, ?, ?)", "Fiquei de pé", 150, 360, 0, 0, 156);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('159', ?, ?, ?, ?, ?, ?)", "Dentinhos crescendo", 150, 360, 1, 0, 157);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('160', ?, ?, ?, ?, ?, ?)", "Tentando caminhar", 150, 360, 1, 0, 158);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('161', ?, ?, ?, ?, ?, ?)", "Meu Sexto mensário", 178, 182, 0, 7, 159);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('162', ?, ?, ?, ?, ?, ?)", "Bati palminha", 180, 360, 0, 0, 160);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('163', ?, ?, ?, ?, ?, ?)", "Batendo palminha", 180, 360, 1, 0, 161);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('164', ?, ?, ?, ?, ?, ?)", "Primeiro dia na creche", 180, 720, 0, 10, 162);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('165', ?, ?, ?, ?, ?, ?)", "Me preparando para creche", 180, 720, 1, 10, 163);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('166', ?, ?, ?, ?, ?, ?)", "Festinha na creche", 180, 720, 1, 10, 164);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('167', ?, ?, ?, ?, ?, ?)", "Mamãe me visitando na creche", 180, 720, 1, 10, 165);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('168', ?, ?, ?, ?, ?, ?)", "Dodói mas logo passa", 180, 360, 1, 0, 166);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('169', ?, ?, ?, ?, ?, ?)", "Fui em um circo", 180, 360, 0, 2, 167);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('170', ?, ?, ?, ?, ?, ?)", "Passeando no zoológico", 180, 720, 1, 2, 168);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('171', ?, ?, ?, ?, ?, ?)", "Cheio de protetor solar", 180, 360, 1, 2, 169);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('172', ?, ?, ?, ?, ?, ?)", "Meu Sétimo mensário", 208, 212, 0, 7, 170);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('173', ?, ?, ?, ?, ?, ?)", "Meu Oitavo mensário", 238, 242, 0, 7, 171);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('174', ?, ?, ?, ?, ?, ?)", "Meu Novo mensário", 268, 272, 0, 7, 172);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('175', ?, ?, ?, ?, ?, ?)", "Meu Décimo mensário", 298, 302, 0, 7, 173);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('176', ?, ?, ?, ?, ?, ?)", "Meu Décimo Primeiro mensário", 328, 333, 0, 7, 174);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('177', ?, ?, ?, ?, ?, ?)", "Meu Primeiro Aninho!", 358, 362, 0, 7, 175);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('178', ?, ?, ?, ?, ?, ?)", "Fui em um museu", 360, 720, 0, 2, 176);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('179', ?, ?, ?, ?, ?, ?)", "Fui no teatro", 360, 720, 0, 2, 177);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('180', ?, ?, ?, ?, ?, ?)", "Vendo filme no cinema", 360, 720, 1, 2, 178);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('181', ?, ?, ?, ?, ?, ?)", "Passeando no circo", 360, 720, 1, 2, 179);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('182', ?, ?, ?, ?, ?, ?)", "Primeiro dia na escolinha", 720, 1440, 0, 10, 180);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('183', ?, ?, ?, ?, ?, ?)", "Minha mochila", 720, 1440, 0, 10, 181);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('184', ?, ?, ?, ?, ?, ?)", "Me preparando para escolinha", 720, 1440, 1, 10, 182);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('185', ?, ?, ?, ?, ?, ?)", "Festinha na escolinha", 720, 1440, 1, 10, 183);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('186', ?, ?, ?, ?, ?, ?)", "Assistindo um teatro", 1440, 2880, 1, 2, 184);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('187', ?, ?, ?, ?, ?, ?)", "Meu 1o Natal", 999, 999, 0, 7, 185);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('188', ?, ?, ?, ?, ?, ?)", "Meu 1o dia das mães", 999, 999, 0, 7, 186);
				connection.ExecuteScalar<EventData>("Insert Into EventData (Id, Description, StartAge, EndAge, Kind, Tag, Priority) values ('189', ?, ?, ?, ?, ?, ?)", "Andei de avião", 999, 999, 0, 2, 187);
			}
			#endregion
		}

		/// <summary>
		/// Finds the events with non used achivments.
		/// </summary>
		/// <returns>The events with non used achivments.</returns>
		public IEnumerable<Event> FindEventsWithNonUsedAchivments()
		{
			var events = m_connection.Query<EventData>("select * from EventData E " +
			             "where (Kind = 0 and not exists (select 1 from MomentData where E.Id = EventId)) " +
			             "or (Kind = 1) Order by Priority");

			return MapperHelper.ToDomainEntities<Event, EventData>(events, Mapper);
		}

	}
}

