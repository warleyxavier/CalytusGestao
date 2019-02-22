using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.Helpers;
using System.IO.Compression;
using System.Windows.Forms;


namespace CalytusGestao.Model.ClassesBD
{
    public class Banco : Controle
    {
        Conexao server = new Conexao();

        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************


        private string endereco = @"C:\Users\Public\Documents\Excellence\CalytusGestao";
        private string erro = null;
        private bool resultado;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************


        public string getEndereco()
        {
            return endereco;
        }

        public string getErro()
        {
            return erro;
        }

        public bool getResultado()
        {
            return resultado;
        }

        // *****************************************************************
        // ********************* CRIAÇÃO DO BANCO **************************
        // *****************************************************************

        public void criaBanco()
        {
            SQLiteConnection conexao = null;

            try
            {
                SQLiteConnection.CreateFile(endereco + @"\Banco\Banco.db");

                conexao = server.conexao();

                StringBuilder sql = new StringBuilder();


                // ***************** USUARIO **********************
                sql.AppendLine("Create table Usuario(usuCodigo integer primary key AUTOINCREMENT not null, usuNome varchar(40) not null, usuUsuario varchar(15) unique not null, usuSenha varchar(60) not null, usuTipo varchar(6) not null );");
                sql.AppendLine("insert into Usuario(usuNome, usuUsuario, usuSenha, usuTipo) values ('Administrador', 'admin', '33354741122871651676713774147412831195', 'Admin');");

                // ***************** ANOTAÇÃO **********************
                sql.AppendLine("Create table Anotacao(anoCodigo integer primary key AUTOINCREMENT not null, anoAssunto varchar(50) not null, anoAnotacao varchar(500) not null, anoData date not null); ");

                // ***************** DOENÇA **********************
                sql.AppendLine("Create table Local(locCodigo integer primary key AUTOINCREMENT not null, locLocal varchar(15) not null); ");
                sql.AppendLine("Insert into local(locLocal) values ('TRONCO'); ");
                sql.AppendLine("Insert into local(locLocal) values ('RAIZ'); ");
                sql.AppendLine("Insert into local(locLocal) values ('FOLHA');");

                sql.AppendLine("Create table Doenca(doeCodigo integer primary key AUTOINCREMENT not null, doeNome varchar(30) not null, doeCaracteristicas varchar(500), doeDiagnostico varchar(500), doeImagem varchar(200)); ");
                sql.AppendLine("Create table locDoenca(lodCodigo integer primary key AUTOINCREMENT not null, lodDoenca integet not null, lodLocal integer not null,  Constraint ChaveEstrangeiraDoencaLocal1 foreign key(lodDoenca) references Doenca(doeCodigo) on delete cascade, Constraint ChaveEstrangeiraDoencaLocal2 foreign key(lodLocal) references Local(locCodigo)); ");

                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Mela de Rhizoctonia'), Upper('Mela das folhas, morte de plantas e crescimento epifítico de micélio sobre estacas;O Fungo em geral não esporula, mas frequentemente produz escleródios sobre o órgão afetado;Sob condições favoráveis, produz himênio branco sobre lesão, seguido pela formação de basídios e basidiósporos, típicos da fase sexual.'), Upper('Controle é feito através de manejo integrado.'), 'doenca1.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Podridão de Cylindrocladium'), Upper('Escurecimento do caule na área lesionada, culminando com a podridão e morte da estaca; Sobre lesão, observa-se esporulação branco-brilhante do fungo; Menos frequentemente, em condições favoráveis formam-se, também estruturas globosas de cor laranja a vermelha do patógeno sobre tecidos mortos;Brotações doentes, substrato infestado e água contaminada, bem como bandejas e tubetes contaminados, constituem as principais fontes de inóculo do patógeno e estacas e miniestacas durante o enraizamento;A identificação das espécies é baseada nas características morfológicas do fungo.'), Upper('Manejo integrado.'),'doenca2.png' );");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Mela de Rhizoctonia'), Upper('Mela das folhas, morte de plantas e crescimento epifítico de micélio sobre estacas;O Fungo em geral não esporula, mas frequentemente produz escleródios sobre o órgão afetado;Sob condições favoráveis, produz himênio branco sobre lesão, seguido pela formação de basídios e basidiósporos, típicos da fase sexual.'), Upper('Controle é feito através de manejo integrado.'), 'doenca3.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Mancha de Hainesia'), Upper('Incide em mudas no viveiro, induzindo lesões foliares circulares, de coloração marrom-clara e contornadas por halo marrom-avermelhado;Causa também anelamento da haste e morte de porções apicais de mudas, estando as lesões associadas às podas realizadas no preparo de estacas e miniestacas.'), Upper('Uma vez que a presença de ferimentos é essencial para o estabelecimento do patógeno, devem-se evitar injúrias nas mudas durante o seu manuseio no viveiro.'), 'doenca4.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Antracnose do eucalipto'), Upper('Manchas foliares de formato circular e coloração marrom-clara a avermelhada, seguidas de desfolha pouco significativa. Maiores danos têm sido observados em clones suscetíveis, causando sintomas de secamento descendente da haste de minicepas. Havendo condições favoráveis, essas lesões progridem, podendo levar ao anelamento da haste e à morte das plantas;Sobre as lesões formam-se massas de esporos de coloração creme a rosada que exsudam dos conidiomas, circundados por setas. A partir de cortes histológicos, são verificados conídios elíptivos do fungo.'), Upper('Promover o espaçamento das mudas de modo a impedir o abafamento das plantas nos canteiros. Em minicepas, evitar podas drásticas de minicepas clonais e abafamento das plantas e adotar outras técnicas de manejo que favoreçam o bom desenvolvimento das plantas e que reduzam as condições favoráveis à enfermidade.'), 'doenca5.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Ferrugem do eucalipto'), Upper('A principal característica para sua diagnose é a esporulação urediniospórica, pulverulenta e de coloração amarela sobre os órgãos afetados;Em materiais altamente suscetíveis, causa deformações, necroses, hipertrofia, minicancros e morte das porções terminais de crescimento;Embora a fase uredinial seja a mais comum e a principal forma de disseminação da doença, em épocas mais quentes teliósporos podem também ser produzidos, ainda que menos frequentemente;Os urediniósporos são unicelulares e os teliósporos são bicelulares;Os teliósporo germinam e produzem basidiósporos.'), Upper('A ampla variabilidade genética inter e intraespecífica para resistência à ferrugem permite o controle da doença por meio do plantio de clones, progênies ou espécies resistentes. Dentre as espécies resistentes, destacam-se Corymbia citriodora, C. torelliana, Eucalyptus camaldulensis, E microcorys, E. pellita, E. urophylla;Há, contudo, ampla variabilidade intraespecífica, o que permite a seleção e clonagem de genótipos resistentes para plantio;O controle da ferrugem pode ser feito por meio do plantio de progênies resistentes, cujas sementes são colhidas em matrizes homozigotas para a resistência. Também é possível selecionar plantas com características de rápido crescimento, escapando-se da doença, uma vez que, em maiores alturas, o patógeno, geralmente, não encontra microambiente favorável à infecção.'), 'doenca6.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Mancha de Cylindrocladium'), Upper('Manchas foliares de dimensões variáveis. As lesões iniciam-se na base, no ápice ou nas margens das folhas. Com o progresso da doença, a lesão pode atingir grande área do limbro foliar e induzir acentuada desfolha nos terços basal, mediano e apical das copas de árvores, a partir do primeiro ano de plantio.'), Upper('O plantio de clones, progênies, procedências ou espécies resistentes constitui a melhor estratégia de controle. A determinação do modelo de herança e da base genética da resistência é fundamental para embasar os programas de melhoramento que visam à obtenção de materiais resistentes.'), 'doenca7.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Mancha de Rhizoctonia'), Upper('Manchas grandes e irregulares em folhas de diferentes idades. Inicialmente, as folhas afetadas apresentam lesões irregulares, de diferentes tamanhos e com uma coloração variando de cinza-claro a marrom-clara, culminando com a queima quase total das folhas que adquirem coloração palha. No início, as folhas infectadas ficam presas à planta, mas tendem a cair com o tempo.'), Upper('O plantio de procedências, espécies ou clones de eucalipto resistentes constitua a melhor estratégia para o manejo da enfermidade no campo. Todavia, inoculações artificiais devem ser conduzidas para testar essa hipótese.'), 'doenca8.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper('Mancha de Bactérias'), Upper('Nas fases iniciais de infecção, a doença caracteriza-se por intensa desfolha, anelamento e morte de porções apicais de materiais altamente suscetíveis e manchas foliares úmidas e translúcidas (anasarca), em consequência do extravasamento de água para os intercelulares.'), Upper('Como há variabilidade quanto à intensidade da doença, os procedimentos mais recomendados são a seleção e o plantio de materiais resistentes. É fundamental determinar o modelo de herança e a base genética da resistência para embasar os programas de melhoramento genético com vistas à obtenção de plantas resistentes'), 'doenca9.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem ) values (Upper('Mancha de Kirramyces'), Upper('Manchas angulares e marrom-arroxeadas, distribuídas em ambas as faces de folhas maduras, sobre as quais é possível observar exsudação de massas de conídios em cordões, cuja coloração negra lembra fumagina.'), Upper('Até o presente, têm sido dispensadas medidas específicas de controle, mas a variabilidade, quanto à resistência em diferentes materiais genéticos indica a possibilidade de seleção e plantio de clones resistentes.'), 'doenca10.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem ) values (Upper('Murcha bacteriana'), Upper('A doença incide em mudas em viveiro e em plantas no campo apartir de 4-8 meses de idade. No campo, caracteriza-se por murcha e necrose na região da nervura central das folhas, bronzeamento e, ou, amarelecimento e desfolha ascendente.'), Upper('Plantio de mudas novas, sadias e bem enfolhadas, com sistema radicular bem formado, sem injúrias mecânicas nas raízes e no coleto e sem afogamento de coleto.'), 'doenca11.png');");
                sql.AppendLine(@"Insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem ) values (Upper('Cancro do eucalipto'), Upper('A infecção pode ser observada, geralmente, a partir do sexto mês de plantio no campo. Ao incidir em plantas jovens ou adultas de pequeno diâmetro, ou em minicepas no jardim clonal, geralmente provoca a morte por anelamento da haste.'), Upper('Plantio de clones ou espécies resistentes. Contudo, é fundamental determinar o modelo de herança e a base genética da resistência ao cancro para embasar os programas de melhoramento para a obtenção de plantas resistentes.'), 'doenca12.png');");

                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (1, 1);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (2, 1);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (3, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (4, 1);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (4, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (5, 1);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (5, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (6, 1);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (6, 2);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (6, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (7, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (8, 1);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (8, 2);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (8, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (9, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (10, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (11, 2);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (11, 3);");
                sql.AppendLine("Insert into locDoenca(lodDoenca, lodLocal) values (12, 1);");

                // ***************** ESPÉCIES EUCALIPTO **********************
                sql.AppendLine("Create table Utilidade(utiCodigo integer primary key AUTOINCREMENT not null, utiUtilidade varchar(30) not null); ");
                sql.AppendLine("Insert into Utilidade(utiUtilidade) values ('CARVÃO VEGETAL');");
                sql.AppendLine("Insert into Utilidade(utiUtilidade) values ('LENHA');");
                sql.AppendLine("Insert into Utilidade(utiUtilidade) values ('CELULOSE');");
                sql.AppendLine("Insert into Utilidade(utiUtilidade) values ('POSTES');");
                sql.AppendLine("Insert into Utilidade(utiUtilidade) values ('ORNAMENTAÇÃO');");
                sql.AppendLine("Insert into Utilidade(utiUtilidade) values ('MADEIRA');");

                sql.AppendLine("Create table Especie(espCodigo integer primary key AUTOINCREMENT, espNomeCientifico varchar(30), espNomePopular varchar(30) not null, espCaracteristicas varchar(500), espImagem varchar(200)); ");
                sql.AppendLine("Create table UtilEspecie(utilEsCodigo integer primary key AUTOINCREMENT not null, utilEsEspecie integer not null, utilEsUtilidade integer not null, constraint ChaveEstrUtEspEspecie foreign key(utilEsEspecie) references Especie(espCodigo) on delete cascade, constraint ChaveEstrUtEspUtilidade foreign key(utilEsUtilidade) references Utilidade(utiCodigo)); ");

                // ***************** PLANTAÇÕES EUCALIPTO **********************
                sql.AppendLine("Create table Plantacao(plaCodigo integer primary key AUTOINCREMENT, plaIdentificacao varchar(50) not null, plaAno integer not null, plaQtdPlantas integer not null, plaLocalizacao varchar(200), plaMunicipio varchar(50), plaTamanhoArea decimal(5, 2) not null, plaQtdAtualPlantas integer not null, plaCustos decimal(5, 2) not null, plaGanhos decimal(5, 2) not null, plaStatus varchar(8) not null, plaImagem string(200)); ");
                sql.AppendLine("Create table PlantacaoEspecie(plECodigo integer primary key AUTOINCREMENT not null, plEPlantacao integer not null, plEEspecie integer not null, constraint ChavEstrPlantEspPlant foreign key(plEPlantacao) references Plantacao(plaCodigo)on delete cascade, constraint ChavEstrPlantEspEspec foreign key(plEEspecie) references Especie(espCodigo)); ");

                // ***************** ATIVIDADES DA PLANTAÇÃO **********************
                sql.AppendLine("Create table Tipo(tipCodigo integer primary key AUTOINCREMENT not null, tipTipo varchar(50) not null); ");
                sql.AppendLine("Create table Atividade(atiCodigo integer primary key AUTOINCREMENT not null, atiPlantacao integer not null, atiTipo integer not null, atiCustos decimal(5, 2) not null, atiGanhos decimal(5, 2) not null, atiData date not null, atiRelatorio varchar(500), constraint ChaveEstrAtiPlant foreign key(atiPlantacao) references Plantacao(plaCodigo) on delete cascade, constraint ChaveEstrAtiTipo foreign key(atiTipo) references Tipo(tipCodigo)); ");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CONSERVAÇÃO DO SOLO');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('LIMPEZA DA ÁREA');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CONTROLE DE FORMIGAS');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CONTROLE DE FORMGAS');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('APLICAÇÃO DE HERBICIDAS');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('ADUBAÇÃO');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CALAGEM');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('PLANTIO');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('IRRIGAÇÃO');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('PODA');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CORTE PARCIAL');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CORTE TOTAL');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('ADQUIRIMENTO DE MUDAS');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('PREPARAMENTO DO TERRENO');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('REPLANTIO');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('DESBASTE');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('PODA');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('CONTROLE DE DOENÇAS');");
                sql.AppendLine("Insert into Tipo(tipTipo) values ('OUTRO');");

                // ***************** ANÁLISE DE SOLO **********************

                sql.AppendLine("Create table Analise( anaCodigo integer PRIMARY KEY AUTOINCREMENT not null, anaData date not null,anaProfundidade decimal(2,2) null, anaPlantacao integer not null, anaPh decimal(5,2) null,anaFosforo decimal(5,2) null,     anaPotassio decimal(5,2) null,anaCalcio decimal(5,2) null,anaMagnesio decimal(5,2) null, anaAluminio decimal(5,2) null,anaCTCt1 decimal(5,2) null,anaCTCt2 decimal(5,2) null,   anaIndSaturacao decimal(5,2) null,anaPRem decimal(5,2) null,anaImagem varchar(200) null, constraint chaveEstrangAnalisePlantacao foreign key(anaPlantacao) references Plantacao(plaCodigo) on delete cascade);");


                SQLiteCommand command = server.command(sql.ToString(), conexao);
                command.ExecuteNonQuery();
                resultado = true;
            }
            catch(SQLiteException e)
            {
                erro = e.Message;
                resultado = false;
            }
            catch (Exception e)
            {
                erro = e.Message;
                resultado = false;
            }
            finally
            {
                conexao.Close();
            }
        }

        // *****************************************************************
        // ********************* CRIAÇÃO DAS PASTAS ************************
        // *****************************************************************

        public void criaPastas()
        {
            
            DirectoryInfo pasta = null;

            pasta = new DirectoryInfo(endereco);
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;

            pasta = new DirectoryInfo(endereco + @"\Banco");
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;

            pasta = new DirectoryInfo(endereco + @"\Imagens");
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;

            pasta = new DirectoryInfo(endereco + @"\Imagens\Doenças");
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;

            pasta = new DirectoryInfo(endereco + @"\Imagens\Especies");
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;

            pasta = new DirectoryInfo(endereco + @"\Imagens\Plantacoes");
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;

            pasta = new DirectoryInfo(endereco + @"\Imagens\Analises");
            pasta.Create();
            pasta.Attributes = FileAttributes.Hidden;
        }

        // *****************************************************************
        // ********************* CRIAÇÃO DE  BACKUP ************************
        // *****************************************************************

        /*public void backup(string origem, string destino)
        {
            try
            {
                DirectoryInfo pasta = new DirectoryInfo(origem);
                DirectoryInfo[] subPastas = pasta.GetDirectories();
                FileInfo[] arquivos = pasta.GetFiles();

                DirectoryInfo pastaDestino = new DirectoryInfo(destino);
                pastaDestino.Create();

                foreach(FileInfo arquivo in arquivos)
                {
                    string caminho = Path.Combine(destino, arquivo.Name);
                    arquivo.CopyTo(caminho);
                }

                foreach(DirectoryInfo sub in subPastas)
                {
                    string caminhoSub = Path.Combine(destino, sub.Name);
                    backup(sub.FullName, caminhoSub);
                }

                resultado = true;
            }
            catch(Exception e)
            {
                resultado = false;
                erro = e.Message;
            }
        }*/

        // *****************************************************************
        // ****************** RESTURAÇÃO DE  BACKUP ************************
        // *****************************************************************

        public void backup(string destino)
        {
            try
            {
                string origem = @"C:\Users\Public\Documents\Excellence\CalytusGestao";
                ZipFile.CreateFromDirectory(origem, destino + ".zip");

                resultado = true;
            }
            catch (Exception e)
            {
                resultado = false;
                erro = e.Message;
            }
        }

        public void restaurar(string origem)
        {
            try
            {
                string destino = @"C:\Users\Public\Documents\Excellence\CalytusGestao";
                ZipFile.ExtractToDirectory(origem, destino);

                resultado = true;
            }
            catch (Exception e)
            {
                resultado = false;
                erro = e.Message;
            }
        }
    }
}
