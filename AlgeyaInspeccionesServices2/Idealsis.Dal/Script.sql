/*Base de datos*/
use ph18527362475_obviam

/*SysConfigValor*/
CREATE TABLE [dbo].[SysConfigValor] (
	[Catalogo]     tinyint  NOT NULL ,
	[Codigo]       varchar  (40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Descripcion]  varchar  (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Modulo]       varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Grupo]        varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Orden]        smallint NULL ,
	[Filtro]       varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Valor]        varchar  (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_sysConfigValor PRIMARY KEY CLUSTERED (Catalogo,Codigo)
)
GO

/*SysConfigEmail*/
CREATE TABLE [dbo].[SysConfigEmail] (
	[Id]              int IDENTITY(1,1) NOT NULL ,
	[Origen]          tinyint  NOT NULL ,
	[IdOrigen]        int      NOT NULL ,
	[Descripcion]     varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Email]           varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ServidorSmtp]    varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Puerto]          varchar (10)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Autentificacion] tinyint NULL ,
	[Usuario]         varchar (70)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Password]        varchar (30)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EmailTest]       varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UrlLogo]         varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ConSsl]          tinyint NULL ,
	[ConCopiaA]       tinyint NULL ,
	[Libreria]        tinyint NULL ,
	[Activo]          tinyint NULL ,
	CONSTRAINT PK_sysConfigEmail PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_SysConfigEmail] ON [dbo].[SysConfigEmail] (Origen,IdOrigen)
GO

/*SegCatalogos*/
CREATE TABLE [dbo].[SegCatalogos] (
	[Id]           int IDENTITY (1, 1) NOT NULL ,
	[Catalogo]     tinyint  NOT NULL ,
	[Codigo]       varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Descripcion]  varchar (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Password]     varchar (20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	[IdPadre]      int      NULL ,
	[IdDireccion]  int      NULL ,
	[Orden]        smallint NULL ,
	[Nivel]        tinyint  NULL ,
	[Icono]        smallint NULL ,
	[NumMensajes]  int      NULL ,
	[Activo]       tinyint  NULL ,
	CONSTRAINT PK_SegCatalogos PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_SegCatalogos] ON [dbo].[SegCatalogos] (Catalogo,Codigo)
GO
INSERT INTO [dbo].[SegCatalogos] (Catalogo,Codigo,IdPadre,Descripcion,Password,Nivel,Activo) VALUES (1,'IDEALSIS',3,'IDEALSIS','IDEALSIS',1,1)
GO
INSERT INTO [dbo].[SegCatalogos] (Catalogo,Codigo,IdPadre,Descripcion,Password,Nivel,Activo) VALUES (2,'Administrador',0,'Administrador','',1,1)
GO
INSERT INTO [dbo].[SegCatalogos] (Catalogo,Codigo,IdPadre,Descripcion,Password,Nivel,Activo) VALUES (3,'Administrador',0,'Administrador','',1,1)
GO

--Consultar catalogos de seguridad
IF OBJECT_ID ( N'dbo.Sp_SegCatalogosListar', N'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.Sp_SegCatalogosListar;
GO
CREATE PROCEDURE dbo.Sp_SegCatalogosListar 
	@Catalogo       int            -- folio de certificado
	WITH ENCRYPTION
AS
	SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM SegCatalogos WHERE Catalogo=@Catalogo ORDER BY Codigo
    SET NOCOUNT OFF;
GO

-- Consultar todos los catalogos a la vez (usuarios,perfiles,grupos)
IF OBJECT_ID ( N'dbo.Sp_SegCatalogosListarTodos', N'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.Sp_SegCatalogosListarTodos;
GO
CREATE PROCEDURE dbo.Sp_SegCatalogosListarTodos 
	WITH ENCRYPTION
AS
	-- Listar usuarios
	SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM SegCatalogos WHERE Catalogo=1 ORDER BY Codigo
	-- Listar grupos
	SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM SegCatalogos WHERE Catalogo=2 ORDER BY Codigo
	-- Listar perfiles
	SELECT Descripcion, Codigo, Id, Password, Nivel, IdPadre, Activo FROM SegCatalogos WHERE Catalogo=3 ORDER BY Codigo
    SET NOCOUNT OFF;
GO

/*SegCatalogosDetalle*/
CREATE TABLE [dbo].[SegCatalogosDetalle] (
	[Id]               int IDENTITY(1,1) NOT NULL ,
	[Usuario]          int   NOT NULL ,
	[Perfil]           int   NOT NULL ,
	[ConVigencia]      tinyint  NULL ,
	[InicioVigencia]   datetime NULL ,
	[FinVigencia]      datetime NULL ,
	[Orden]            smallint NULL ,
	CONSTRAINT PK_SegCatalogosDetalle PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_SegCatalogosDetalle ON [dbo].[SegCatalogosDetalle] (Usuario,Perfil)
GO
INSERT INTO [dbo].[SegCatalogosDetalle] (Usuario,Perfil,ConVigencia,InicioVigencia,FinVigencia,Orden) VALUES (1,2,0,NULL,NULL,1)
GO

/*SegCatalogosRestricciones*/
CREATE TABLE [dbo].[SegCatalogosRestricciones] (
	[Id]           int IDENTITY (1, 1) NOT NULL ,
	[Usuario]      int      NOT NULL ,
	[Perfil]       int      NOT NULL ,
	[Tipo]         smallint NOT NULL ,
	[Catalogo]     smallint NOT NULL ,
	[Codigo]       int      NOT NULL ,
	[Cuenta]       varchar (32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Descripcion]  varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Padre]        int      NULL ,
	[Valor]        smallint NULL ,
	[EsPred]       tinyint  NULL ,
	[Posicion]     smallint NULL ,
	CONSTRAINT PK_SegCatalogosRestricciones PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_SegCatalogosRestricciones ON [dbo].[SegCatalogosRestricciones] (Usuario,Perfil,Tipo,Catalogo,Codigo,Cuenta)
GO

/*SegCatalogosOpciones*/
CREATE TABLE [dbo].[SegCatalogosOpciones] (
	[Id]          int IDENTITY(1,1) NOT NULL ,
	[Usuario]     int   NOT NULL ,	
	[Perfil]      int   NOT NULL ,
	[Tipo]        varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
	[Codigo]      varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
	[SoloLectura] tinyint  NULL ,
	CONSTRAINT PK_SegCatalogosOpciones PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_SegCatalogosOpcioness ON [dbo].[SegCatalogosOpciones] (Usuario,Perfil,Tipo,Codigo)
GO
INSERT INTO [dbo].[SegCatalogosOpciones] (Usuario,Perfil,Tipo,Codigo,SoloLectura) VALUES (1,2,'Menu','MnuSeguridad',0)
GO
INSERT INTO [dbo].[SegCatalogosOpciones] (Usuario,Perfil,Tipo,Codigo,SoloLectura) VALUES (1,2,'Menu','MnuSegGrupos',0)
GO
INSERT INTO [dbo].[SegCatalogosOpciones] (Usuario,Perfil,Tipo,Codigo,SoloLectura) VALUES (1,2,'Menu','MnuSegPerfiles',0)
GO
INSERT INTO [dbo].[SegCatalogosOpciones] (Usuario,Perfil,Tipo,Codigo,SoloLectura) VALUES (1,2,'Menu','MnuSegUsuarios',0)
GO


/*SegOpciones*/
CREATE TABLE [dbo].[SegOpciones] (
	[Tipo]        varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
	[Codigo]      varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
	[Padre]       varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	[Descripcion] varchar (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Origen]      smallint NULL ,
	[Catalogo]    smallint NULL ,
	[EsPermiso]   tinyint  NULL ,
	[Orden]       smallint NULL ,
	CONSTRAINT PK_segOpciones PRIMARY KEY CLUSTERED (Tipo,Codigo)
)
GO

INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatalogos','','Catalogos',0,0,0,1)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatEntidadFinanciera','MnuCatalogos','Tipos de entidades',1,79,0,2)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatTiposAuditoria','MnuCatalogos','Tipos de auditorias',1,80,0,3)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatAuditores','MnuCatalogos','Auditores',1,0,0,4)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatListasNegras','MnuCatalogos','Tipos de lista negras',1,82,0,5)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatHallazgos','MnuCatalogos','Tipos de hallazgos',1,83,0,6)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatEstatus','MnuCatalogos','Estatus de auditoria',1,84,0,7)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatTiposReporte','MnuCatalogos','Tipos de reportes',1,85,0,8)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatAutoridades','MnuCatalogos','Autoridades',1,86,0,9)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatTiposPersona','MnuCatalogos','Tipos de persona',1,87,0,10)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatCargosCCC','MnuCatalogos','Cargos en comite de comunicación y control',1,90,0,11)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatAreas','MnuCatalogos','Areas',1,91,0,12)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatBancos','MnuCatalogos','Bancos',1,56,0,13)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatMonedas','MnuCatalogos','Monedas',0,0,0,14)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatAreasAuditoria','MnuCatalogos','Areas de auditoria',1,92,0,15)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatEstatusCump','MnuCatalogos','Estatus de cumplimiento',1,93,0,16)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatEstatusReq','MnuCatalogos','Estatus de requerimientos',1,95,0,17)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatEstatusHallazgos','MnuCatalogos','Estatus de hallazgos',1,99,0,19)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatMotivoOficio','MnuCatalogos','Motivos de oficios',1,100,0,19)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatEstatusOficio','MnuCatalogos','Estatus de oficios',1,101,0,19)
GO



INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuClientes','','Clientes',0,0,0,100)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatInstrumentos','MnuClientes','Instrumentos',1,89,0,101)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatPuestos','MnuClientes','Puestos',1,88,0,102)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatSistemas','MnuClientes','Sistemas',0,0,0,103)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatPersonas','MnuClientes','Personas relacionadas',0,0,0,104)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatClientes','MnuClientes','Clientes',0,0,0,105)
GO


INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpedientes','','Auditorias',0,0,0,150)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpedienteBuscar','MnuExpedientes','Alta y consulta de auditorias',0,0,0,151)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','PerBorrarAuditoria','MnuExpedientes','Borrar autoria',0,0,1,152)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpRequerimientos','MnuExpedientes','Seguimiento de requerimientos',0,0,0,153)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpOpeRelevantes','MnuExpedientes','Operaciones relevantes (Papel de trabajo)',0,0,0,154)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpTemas','MnuExpedientes','Temas de minutas',1,97,0,155)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpMinutas','MnuExpedientes','Minutas',0,0,0,156)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpHallazgos','MnuExpedientes','Hallazgos',0,0,0,157)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpOficios','MnuExpedientes','Oficios',0,0,0,158)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpTest','MnuExpedientes','Test de listas negras',0,0,0,159)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpReportes','MnuExpedientes','Reportes enviados',0,0,0,160)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpAcuses','MnuExpedientes','Acuses',0,0,0,161)
GO

INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpPersonaFisica','MnuExpedientes','Expediente cliente por persona fisica',0,0,0,162)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpPersonaMoral','MnuExpedientes','Expediente cliente por persona moral',0,0,0,163)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpPersonaEmpleado','MnuExpedientes','Expediente cliente por empleados',0,0,0,164)
GO


INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpTipoPersona','MnuExpedientes','Expediente cliente por tipo de persona',0,0,0,162)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpPersonaFisica','MnuExpTipoPersona','Persona fisica',0,0,0,163)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpPersonaMoral','MnuExpTipoPersona','Persona moral',0,0,0,164)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpPersonaEmpleado','MnuExpTipoPersona','Empleados',0,0,0,165)
GO



INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuLineamientos','','Disposiciones de Carácter General',0,0,0,180)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento0','MnuLineamientos','Enfoque Basa en Riesgo',3,1,0,181)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento1','MnuLineamientos','Identificacion de cliente o usuario',3,2,0,182)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento2','MnuLineamientos','Conocimiento  de Cliente o Usuario',3,3,0,183)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento3','MnuLineamientos','De la presentación de Reportes a la Autoridad',3,4,0,184)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento4','MnuLineamientos','Estructuras internas',3,5,0,185)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento5','MnuLineamientos','De la capacitación y difusión',3,6,0,186)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento6','MnuLineamientos','Sistema Automatizado',3,7,0,187)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento7','MnuLineamientos','En relación con los empleados que laboren en áreas de atención al público o de administración de recursos',3,8,0,188)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento8','MnuLineamientos','De la conservación de la información',3,9,0,189)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento9','MnuLineamientos','De las listas, deberá contener al menos, si el Sujeto Obligado',3,10,0,190)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento10','MnuLineamientos','De otra información',3,11,0,191)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento11','MnuLineamientos','Reserva y confidencialidad',3,12,0,192)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuExpLineamiento12','MnuLineamientos','Modelos novedosos',3,13,0,193)
GO



INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuConfig','','Configuración',0,0,0,200)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatDatos','MnuConfig','Datos',2,1,0,201)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuEntidadDetalle','MnuConfig','Configuración de entidades',0,0,0,202)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuTipoPersonaDetalle','MnuConfig','Configuración de expediente cliente por tipo de persona',0,0,0,203)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuTipoAuditoriaDetalle','MnuConfig','Configuración tipo de auditoria',0,0,0,204)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuCatModelos','MnuConfig','Documentos modelo',2,1,0,205)
GO


INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuSeguridad','','Seguridad',0,0,0,500)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuSegGrupos','MnuSeguridad','Grupos de usuarios',0,0,0,501)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuSegPerfiles','MnuSeguridad','Perfiles de usuarios',0,0,0,502)
GO
INSERT INTO [dbo].[SegOpciones] (Tipo,Codigo,Padre,Descripcion,Origen,Catalogo,EsPermiso,Orden) VALUES ('Menu','MnuSegUsuarios','MnuSeguridad','Usuarios',0,0,0,503)
GO

/*SegSesionesActivas*/
CREATE TABLE [dbo].[SegSesionesActivas] (
	[Usuario]   int    NOT NULL ,
	[Pc]        varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Fecha]     datetime NULL ,
	[Hora]      datetime NULL ,
	[Spid]      int      NULL ,
	CONSTRAINT PK_SegSesionesActivas PRIMARY KEY CLUSTERED (Usuario,Pc)
)
GO

/*CatMonedas*/
CREATE TABLE [dbo].[CatMonedas] (
	[Codigo]      smallint NOT NULL ,
	[Descripcion] varchar (40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Simbolo]     varchar (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Texto]       varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EsPred]      tinyint NULL ,
	[EsNacional]  tinyint NULL ,
	[ClaveSat]    varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_catMonedas PRIMARY KEY CLUSTERED (Codigo)
)
GO
INSERT INTO [dbo].[CatMonedas] (Codigo,Descripcion,Simbolo,Texto,EsPred,EsNacional,ClaveSat) VALUES(1,'PESOS','Pesos','Pesos/m.n.',1,1,'MXN')
GO
INSERT INTO [dbo].[CatMonedas] (Codigo,Descripcion,Simbolo,Texto,EsPred,EsNacional,ClaveSat) VALUES(2,'DOLARES','U.S.','Dolar',0,0,'USD')
GO

/*CatMonedasTiposDeCambio*/
CREATE TABLE [dbo].[CatMonedasTiposDeCambio] (
	[Moneda]       smallint NOT NULL ,
	[Fecha]        datetime NOT NULL ,
	[Usuario]      varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Hora]         datetime NULL ,
	[TipoDeCambio] float NULL ,
	CONSTRAINT PK_catMonedasTiposDeCambio PRIMARY KEY CLUSTERED (Moneda,Fecha)
)
GO

/*SysAdjuntosArchivo*/
CREATE TABLE [dbo].[SysAdjuntosArchivo] (
	[Id]        int IDENTITY (1, 1) NOT NULL ,
	[Bandera]   tinyint NULL ,
	[Archivo]   varbinary(max) NULL ,
	[Extension] varchar (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_sysAdjuntosArchivo PRIMARY KEY CLUSTERED (Id)
)
GO

/*SysAdjuntosRelacion*/
CREATE TABLE [dbo].[SysAdjuntosRelacion] (
	[Catalogo]    tinyint  NOT NULL ,
	[IdRelacion]  int      NOT NULL ,
	[Posicion]    smallint NOT NULL ,
	[Tipo]        int      NULL ,
	[Descripcion] varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Ruta]        varchar (350) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdArchivo]   int      NULL ,
	[FechaAlta]   datetime NULL  ,
	[UsuarioAlta] varchar  (25)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FechaMod]    datetime NULL  ,
	[UsuarioMod]  varchar  (25)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Nombre]      varchar  (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Extension]   varchar  (10)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_sysAdjuntosRelacion PRIMARY KEY CLUSTERED (Catalogo,IdRelacion,Posicion)
)
GO

/*SysCatalogosConfig*/
CREATE TABLE [dbo].[SysCatalogosConfig] (
	[Codigo]      smallint NOT NULL ,
	[Descripcion] varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Etiqueta]    varchar (40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Genero]      tinyint  NULL ,
	[ConCodigo]   tinyint  NULL ,
	[ConEstatus]  tinyint  NULL ,
	[ConCorta]    tinyint  NULL ,
	[Bandera]     tinyint  NULL ,
	[Detalle]     tinyint  NULL ,
	[ConPred]     tinyint  NULL ,
	[CatSat]      smallint NULL ,
	[CatPadre]    smallint NULL ,
	[CatHijo]     smallint NULL ,
	[MenuHijo]    varchar (40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_sysCatalogosConfig PRIMARY KEY CLUSTERED (Codigo)
)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta) VALUES(1,'Tipos de documento','Tipo de documento')
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo) VALUES(29,'Motivos de cancelacion','Motivo',1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConEstatus,ConPred) VALUES(33,'Politicas de precios','Politicas de precio',1,1,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConEstatus,ConPred) VALUES(34,'Tipos de cliente','Tipo de cliente',1,0,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta) VALUES(35,'Clasificación de articulos','Clasificacion',1,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta) VALUES(36,'Marcas de articulos','Marca',1,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta) VALUES(37,'Grupos de lineas','Grupo',1,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,Bandera,CatHijo,MenuHijo) VALUES(38,'Lineas','Linea',1,1,3,37,'MnuCatGruposDeLinea')
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre) VALUES(39,'Sublineas','Sublinea',1,1,38)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatSat) VALUES(40,'Paises','Pais',1,1,8)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta) VALUES(41,'Entidades federativas','Estado',1,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,CatHijo,MenuHijo) VALUES(44,'Poblaciones','Poblacion',1,41,'MnuCatEstados')
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,Bandera) VALUES(45,'Avisos','Aviso',1,2)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatSat,Detalle) VALUES(49,'Unidades de medida','Unidad',1,1,11,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatSat,Bandera) VALUES(51,'Impuestos','Impuesto',1,0,12,5)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,CatPadre) VALUES(54,'Municipios','Municipio',1,41)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,CatPadre,Bandera) VALUES(55,'Colonias','Colonia',1,54,7)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatSat,Bandera) VALUES(56,'Bancos','Banco',1,1,0,8)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(73,'Tipos de persona','Tipo de persona',1,0,0,0)
GO

INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(79,'Tipos de entidades','Entidad',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(80,'Tipos de auditorias','Tipo auditoria',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(81,'Auditores','Auditores',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(82,'Listas negras','Lista negra',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(83,'Tipos de hallazgos','Tipo hallazgo',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(84,'Estatus de auditoria','Estatus auditoria',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(85,'Tipos de reportes','Tipo de reporte',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(86,'Autoridades','Autoridad',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(87,'Tipos de personas','Tipo de persona',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(88,'Puestos','Puesto',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(89,'Productos','Producto',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(90,'Cargos en comite de comunicación y control','Cargo',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(91,'Areas CCC','Area CCC',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(92,'Areas de auditoria','Area',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(93,'Estatus de cumplimiento','Estatus',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(94,'Lineamientos de auditoria','Lineamiento',1,1,0,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(95,'Estatus de requerimientos','Estatus',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera) VALUES(96,'Tipos de sesion','Tipo',1,0,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera,ConPred) VALUES(97,'Temas de minutas','Tema',1,0,1000,0,0)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera,ConPred) VALUES(99,'Estatus de hallazgos','Estatus',1,0,0,0,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera,ConPred) VALUES(100,'Motivos de oficios','Motivo',1,0,0,0,1)
GO
INSERT INTO [dbo].[SysCatalogosConfig] (Codigo,Descripcion,Etiqueta,ConCodigo,ConCorta,CatPadre,Bandera,ConPred) VALUES(101,'Estatus de oficios','Estatus',1,0,0,0,1)
GO



/*SysCatalogos*/
CREATE TABLE [dbo].[SysCatalogos] (
	[Id]          int IDENTITY (1, 1) NOT NULL ,
	[Catalogo]    smallint NOT NULL ,
	[Codigo]      smallint NOT NULL ,
	[Descripcion] varchar (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Corta]       varchar (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ClaveSat]    varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ValorNum]    int     NULL ,
	[ValorDec]    float   NULL ,
	[ValorStr]    varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdPadre]     int      NULL ,
	[IdHijo]      int      NULL ,
	[Bandera1]    tinyint  NULL ,
	[Bandera2]    tinyint  NULL ,
	[EsPred]      tinyint  NULL ,
	[Activo]      tinyint  NULL ,
	CONSTRAINT PK_sysCatalogos PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_sysCatalogos] ON [dbo].[SysCatalogos] (Catalogo,Codigo)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(12,1,'Imagen')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(12,2,'Documento pdf')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(12,3,'Documento Excel')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(12,4,'Documento Word')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,1,'AGUASCALIENTES','AGS')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,2,'BAJA CALIFORNIA NORTE','BCN')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,3,'BAJA CALIFORNIA SUR','BCS')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,4,'CAMPECHE','CAM')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,5,'COAHUILA','COA')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,6,'COLIMA','COL')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,7,'CHIAPAS','CHI')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,8,'CHIHUAHUA','CHIHUA')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,9,'DISTRITO FEDERAL','DF')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,10,'DURANGO','DGO')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,11,'GUANAJUATO','GTO')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,12,'GUERRERO','GRO')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,13,'HIDALGO','HGO')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,14,'JALISCO','JAL')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,15,'ESTADO DE MEXICO','EDO MEX')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,16,'MICHOACAN','MICH')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,17,'MORELOS','MOR')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,18,'NAYARIT','NAY')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,19,'NUEVO LEON','NL')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,20,'OAXACA','OAX')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,21,'PUEBLA','PUE')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,22,'QUERETARO','QTO')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,23,'QUINTANA ROO','QRO')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,24,'SAN LUIS POTOSI','SLP')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,25,'SINALOA','SIN')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,26,'SONORA','SON')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,27,'TABASCO','TAB')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,28,'TAMAULIPAS','TAM')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,29,'TLAXCALA','TLA')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,30,'VERACRUZ','VER')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,31,'YUCATAN','YUC')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo, Codigo, Descripcion, Corta) VALUES(41,32,'ZACATECAS','ZAC')
GO

INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(73,1,'Cliente')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(73,2,'Chofer')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(73,3,'Proveedor')
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Descripcion) VALUES(73,4,'Empleado')
GO

INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,1,'Etapa 1','Enfoque Basa en Riesgo.',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,2,'Etapa 2','Identificacion de Cliente o Usuario.',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,3,'Etapa 3','Conocimiento  de Cliente o Usuario.',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,4,'Etapa 4','De la presentación de Reportes a la Autoridad',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,5,'Etapa 5','Estructuras internas.',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,6,'Etapa 6','De la capacitación y difusión deberá contener, si el Sujeto Obligado',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,7,'Etapa 7','Sistema Automatizado',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,8,'Etapa 8','En relación con los empleados que laboren en áreas de atención al público o de administración de recursos',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,9,'Etapa 9','De la conservación de la información',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,10,'Etapa 10','De las listas, deberá contener al menos, si el Sujeto Obligado',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,11,'Etapa 11','De otra información',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,12,'Etapa 12','Reserva y confidencialidad',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(94,13,'Etapa 13','Modelos novedosos',1)
GO


INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(96,1,'Ordinaria','Ordinaria',1)
GO
INSERT INTO [dbo].[SysCatalogos] (Catalogo,Codigo,Corta,Descripcion,Bandera1) VALUES(96,2,'Extraord','Extraordinaria',1)
GO

--Consultar catalogos de dinamicos
IF OBJECT_ID ( N'dbo.Sp_SysCatalogosListar', N'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.Sp_SysCatalogosListar;
GO
CREATE PROCEDURE dbo.Sp_SysCatalogosListar 
	@Catalogo       smallint            -- tipo de catalogo
	WITH ENCRYPTION
AS
	SELECT Id,Catalogo,Codigo,Descripcion,Corta,IdPadre,IdHijo,EsPred,ValorStr,ClaveSat,Bandera1,Bandera2,Activo FROM SysCatalogos WHERE Catalogo=@Catalogo ORDER BY Codigo
    SET NOCOUNT OFF;
GO

--Consultar catalogos de dinamicos
IF OBJECT_ID ( N'dbo.Sp_SysCatalogosListarPadre', N'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.Sp_SysCatalogosListarPadre;
GO
CREATE PROCEDURE dbo.Sp_SysCatalogosListarPadre 
	@Catalogo      smallint,      -- tipo de catalogo
	@IdPadre       int            -- id padre
	WITH ENCRYPTION
AS
	SELECT Id,Catalogo,Codigo,Descripcion,Corta,IdPadre,IdHijo,EsPred,ValorStr,ClaveSat,Bandera1,Bandera2,Activo FROM SysCatalogos WHERE Catalogo=@Catalogo and IdPadre=@IdPadre ORDER BY Codigo
    SET NOCOUNT OFF;
GO

/*SysCatalogosDetalle*/
CREATE TABLE [dbo].[SysCatalogosDetalle] (
	[Id]            int IDENTITY (1, 1) NOT NULL ,
	[IdOrigen]      int      NOT NULL ,
	[Tipo]          smallint NOT NULL ,
	[Posicion]      smallint NOT NULL ,
	[IdArticulo]    int      NULL ,
	[IdPersona]     int      NULL ,
	[IdCatalogo]    int      NULL ,
	[IdDato]        int      NULL ,
	[Cantidad]      money    NULL ,
	[Valor]         float    NULL ,
    [Descripcion]   nvarchar(max)  NULL ,
    [Texto]         nvarchar(max)  NULL ,
    [Notas]         nvarchar(max)  NULL ,
	[EsRequerido]   tinyint  NULL ,
	CONSTRAINT PK_sysCatalogosDetalle PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_sysCatalogosDetalle] ON [dbo].[SysCatalogosDetalle] (IdOrigen,Tipo,Posicion)
GO

/*SysDatos*/
CREATE TABLE [dbo].[SysDatos] (
	[Id]          int IDENTITY (1, 1) NOT NULL ,
	[Catalogo]    smallint NOT NULL ,
	[Codigo]      int      NOT NULL ,
	[Descripcion] varchar  (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EsEtiqueta]  tinyint  NULL ,
	[Tipo]        tinyint  NULL ,
	[Formato]     smallint NULL ,
	[FormatoDes]  varchar  (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FormatoCap]  varchar  (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_sysDatos PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_SysDatos] ON [dbo].[SysDatos] (Catalogo,Codigo)
GO

/*SysDatosOpciones*/
CREATE TABLE [dbo].[SysDatosOpciones] (
	[Dato]        int      NOT NULL ,
	[Posicion]    smallint NOT NULL ,
	[Descripcion] varchar (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_SysDatosOpciones PRIMARY KEY CLUSTERED (Dato,Posicion)
)
GO

/*SysDatosOrden*/
CREATE TABLE [dbo].[SysDatosOrden] (
	[Origen]      smallint NOT NULL ,
	[IdOrigen]    int      NOT NULL ,
	[Dato]        int      NOT NULL ,
	[Orden]       int      NULL ,
	[EsRequerido] tinyint  NULL ,
	CONSTRAINT PK_sysDatosOrden PRIMARY KEY CLUSTERED (Origen,IdOrigen,Dato)
)
GO

/*SysDatosValor*/
CREATE TABLE [dbo].[SysDatosValor] (
	[Origen]      int      NOT NULL ,
	[IdOrigen]    int      NOT NULL ,
	[Dato]        int      NOT NULL ,
	[ValorNumero] money    NULL ,
	[ValorCadena] varchar  (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ValorFecha]  datetime NULL ,
	CONSTRAINT PK_sysDatosValor PRIMARY KEY CLUSTERED (Origen,IdOrigen,Dato)
)
GO

/*SysConfigEmail*/
CREATE TABLE [dbo].[SysConfigEmail] (
	[Id]              int IDENTITY(1,1) NOT NULL ,
	[Origen]          tinyint  NOT NULL ,
	[IdOrigen]        int      NOT NULL ,
	[Descripcion]     varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Email]           varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ServidorSmtp]    varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Puerto]          varchar (10)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Autentificacion] tinyint NULL ,
	[Usuario]         varchar (70)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Password]        varchar (30)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EmailTest]       varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UrlLogo]         varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ConSsl]          tinyint NULL ,
	[ConCopiaA]       tinyint NULL ,
	[Libreria]        tinyint NULL ,
	[Activo]          tinyint  NULL ,
	CONSTRAINT PK_sysConfigEmail PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_SysConfigEmail] ON [dbo].[SysConfigEmail] (Origen,IdOrigen)
GO

/*SysCatalogosSatConfig*/
CREATE TABLE [dbo].[SysCatalogosSatConfig] (
	[Codigo]      smallint NOT NULL ,
	[Clave]       varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Descripcion] varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Etiqueta1]   varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Etiqueta2]   varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Comentarios] varchar (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_sysCatalogosSatConfig PRIMARY KEY CLUSTERED (Codigo)
)
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(1,'c_FormaPago','Formas de pago','','','claves que identifican la forma de pago de los bienes o servicios amparados por el comprobante')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(2,'c_Moneda','Monedas','','','claves para identificar la moneda utilizada para expresar los montos en el comprobante')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(3,'c_TipoDeComprobante','Tipo de comprobante','','','claves que expresan el efecto del comprobante fiscal para el contribuyente emisor')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(4,'c_MetodoPago','Metodo de pago','','','claves que precisan el metodo de pago que aplica al CFDI')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(5,'c_CodigoPostal','Lugar de expedicion','','','c�digo postal del lugar de expedicion del comprobante')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(6,'c_TipoRelacion','Tipo de relacion','','','claves de la relacion que existe entre el comprobante que se est� generando y el o los CFDI�s previos')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(7,'c_RegimenFiscal','Regimen fiscal','','','claves del regimen fiscal del contribuyente emisor al que aplicara el efecto fiscal del comprobante')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(8,'c_Pais','Paises','','','claves del pais de residencia para efectos fiscales del receptor del comprobante')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(9,'c_UsoCFDI','Uso del CFDI','','','claves del uso que se le dara a la factura el receptor del comprobante')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(10,'c_ClaveProdServ','Producto o servicio','','','claves del producto o del servicio amparado por el concepto')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(11,'c_ClaveUnidad','Unidad de medida','','','claves de las unidades de medida estandarizadas aplicable para la cantidad expresada en el concepto')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(12,'c_Impuesto','Tipo de impuesto','','','claves del tipo de impuesto trasladado aplicable al concepto')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(13,'c_TipoFactor','Tipo de factor','','','claves del tipo de factor que se aplica a la base del impuesto')
GO
INSERT INTO [dbo].[SysCatalogosSatConfig] (Codigo,Clave,Descripcion,Etiqueta1,Etiqueta2,Comentarios) VALUES(14,'c_TasaOCuota','Tasa o cuota','','','valor de la tasa o cuota del impuesto que se traslada en el concepto')
GO

/*SysCatalogosSat*/
CREATE TABLE [dbo].[SysCatalogosSat] (
	[Id]          int      IDENTITY(1,1) NOT NULL ,
	[Catalogo]    smallint NOT NULL ,
	[Codigo]      varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
	[Descripcion] varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Valor1]      varchar (20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	[Valor2]      varchar (20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	[EsPred]      tinyint NULL ,
	CONSTRAINT PK_sysCatalogosSat PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_sysCatalogosSat ON [dbo].[SysCatalogosSat] (Catalogo,Codigo)
GO

/*CatPersonas*/
CREATE TABLE [dbo].[CatPersonas] (
	[Id]                int IDENTITY(1,1) NOT NULL ,
	[TipoDePersona]     tinyint NOT NULL ,
	[IdPadre]           int     NOT NULL ,
	[Codigo]            int     NOT NULL ,
	[Descripcion]       varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ApellidoPaterno]   varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ApellidoMaterno]   varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Nombres]           varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Corta]             varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ObjetoSocial]      nvarchar (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FechaConstitucion] datetime NULL ,
	[FolioEscritura]    varchar (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[RegistroCondusef]  varchar (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Agrupador]         int     NULL ,
	[TipoAdmin]         tinyint NULL ,
	[RequiereCCC]       tinyint NULL ,
	[SinAgrupador]      tinyint NULL ,
	[EsAgrupador]       tinyint NULL ,
	[EsTransportista]   tinyint NULL ,
	[EsDistribuidor]    tinyint NULL ,
	[OkObsequios]       tinyint NULL ,
	[Sistema]           int     NULL ,
	[Puesto]            int     NULL ,
	[Tipo]              int     NULL ,
	[Zona]              int     NULL ,
	[Ruta]              int     NULL ,
	[Orden]             int     NULL ,
	[Representante]     int     NULL ,
	[Oficial]           int     NULL ,
	[ReqOrden]          tinyint NULL ,
	[ReqRecepcion]      tinyint NULL ,
	[ReqIDeposito]      tinyint NULL ,
	[IdQuienFactura]    int     NULL ,
	[AplicaRedondeo]    tinyint NULL ,
	[DescripcionFac]    varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EsMoral]           varchar (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IdDireccion]       int     NULL,
	[ContactoVta]       varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ContactoCob]       varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[RevisionPagos]     varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Situacion]         smallint NULL ,
	[AccionLimiteExedido] smallint NULL ,
	[AccionSaldoVencido]  smallint NULL ,
	[Moneda]            smallint NULL ,
	[Politica]          int      NULL ,
	[Condicion]         tinyint  NULL ,
	[Cobrador]          int      NULL ,
	[Vendedor]          int      NULL ,
	[Diacobranza]       int      NULL ,
	[Diarevision]       int      NULL ,
	[Plazo]             smallint NULL ,
	[Limite]            money    NULL ,
	[LimiteDisponible]  money    NULL ,
	[CreditoBloqueado]  tinyint  NULL ,
	[Saldo]             money    NULL ,
	[SaldoVencido]      money    NULL ,
	[PreciosEnUsd]      tinyint  NULL ,
	[CobrarImpuestos]   tinyint  NULL ,
	[RetenerImpuestos]  tinyint  NULL ,
	[ImpuestoFijo]      smallint NULL ,
	[RetencionFija]     smallint NULL ,
	[FechaDeAlta]       datetime NULL ,
	[Activo]            tinyint  NULL ,
	[Consumo]           tinyint  NULL ,
	[EnvioAutDeEmail]   tinyint  NULL ,
	[UsoDeCfdi]         varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AdendaTipo]        smallint NULL ,
	[AdendaSucursal]    varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AdendaProveedor]   varchar (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ComplementoTipo]   smallint NULL ,
	[IdLogo]            int     NULL ,
	[NumLicencia]       varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[VenLicencia]       datetime NULL ,
	[Tarifa]            int      NULL ,
	[NumInsidencias]    smallint NULL ,
	[ConBeneficiario]   tinyint  NULL ,
	[Serie]             varchar (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Folio]             int      NULL ,
	[EsTercero]         tinyint  NULL ,
	[EstaAPrueba]       tinyint  NULL ,
	[InicioPrueba]      datetime NULL ,
	[DiasPrueba]        smallint NULL ,
	[SinComision]       tinyint  NULL ,
	[FechaSinCom]       datetime NULL ,
	[SerieFactura]      varchar (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Comision]          smallint NULL ,
	[Password]          varchar (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[VerEnVentas]       tinyint  NULL ,
	[VerEnCompras]      tinyint  NULL ,
	[ReqAutorizacion]   tinyint  NULL ,
	[CodigoScd]         int      NULL ,
	[EsPred]            tinyint  NULL ,
	CONSTRAINT PK_catPersonas PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_catPersonas0 ON [dbo].[CatPersonas] (TipoDePersona,IdPadre,Codigo)
GO
CREATE NONCLUSTERED INDEX IX_catPersonas1 ON [dbo].[CatPersonas] (Descripcion)
GO

/*CatPersonasDetalle*/
CREATE TABLE [dbo].[CatPersonasDetalle] (
	[Id]            int IDENTITY (1, 1) NOT NULL ,
	[IdOrigen]      int      NOT NULL ,
	[Tipo]          smallint NOT NULL ,
	[Posicion]      smallint NOT NULL ,
    [Codigo]        varchar(10)  NULL ,
    [Descripcion]   varchar(250) NULL ,
	[IdPersona]     int      NULL ,
	[IdCatalogo]    int      NULL ,
	[IdDato]        int      NULL ,
	[Valor]         float    NULL ,
    [Notas]         nvarchar(max)  NULL ,
	[Bandera]       tinyint  NULL ,
	CONSTRAINT PK_CatPersonasDetalle PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_CatPersonasDetalle] ON [dbo].[CatPersonasDetalle] (IdOrigen,Tipo,Posicion)
GO
IF OBJECT_ID ( N'dbo.GuardarPersonasDetalle', N'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.GuardarPersonasDetalle;
GO
CREATE PROCEDURE dbo.GuardarPersonasDetalle 
	@IdOrigen    int, 
	@DetalleJson nvarchar(max)
	WITH ENCRYPTION
	
AS
	DECLARE @Existe      tinyint = 0;
	DECLARE @Id          int;
	DECLARE @Tipo        smallint;
	DECLARE @Posicion    smallint;
	declare @Codigo      nvarchar(10);
	DECLARE @Descripcion nvarchar(250);
	DECLARE @IdPersona   int;
	DECLARE @IdCatalogo  int;
	DECLARE @IdDato      int;
	DECLARE @Valor       float;
	DECLARE @Notas       nvarchar(250);
	DECLARE @Bandera     tinyint;
	
	DELETE FROM CatPersonasDetalle WHERE IdOrigen=@IdOrigen;

	DECLARE TABLA CURSOR FOR SELECT * FROM OPENJSON(@DetalleJson)
		WITH ( 
		Tipo        smallint      '$.Tipo',
		Posicion    smallint      '$.Posicion',
		Codigo      nvarchar(10)  '$.Codigo',
		Descripcion nvarchar(250) '$.Descripcion',
		IdPersona   int           '$.IdPersona',
		IdCatalogo  int           '$.IdCatalogo',
		IdDato      int           '$.IdDato',
		Valor       float         '$.Valor',
		Notas       nvarchar(250) '$.Notas',
		Bandera     tinyint       '$.Bandera'
		)
	OPEN TABLA;
	FETCH NEXT FROM TABLA INTO @Tipo,@Posicion,@Codigo,@Descripcion,@IdPersona,@IdCatalogo,@IdDato,@Valor,@Notas,@Bandera;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			
			SELECT @Id=Id FROM [CatPersonasDetalle] WHERE IdOrigen=@IdOrigen and Tipo=@Tipo and Posicion=@Posicion;
			IF (@Id>0)
				UPDATE CatPersonasDetalle SET Codigo=@Codigo, 
					   Descripcion=@Descripcion,
					   IdPersona=@IdPersona,
					   IdCatalogo=@IdCatalogo,
					   IdDato=@IdDato,
					   Valor=@Valor,
					   Notas=@Notas,
					   Bandera=@Bandera 
					   WHERE Id=@Id;
			ELSE
				INSERT INTO CatPersonasDetalle (IdOrigen,Tipo,Posicion,Codigo,Descripcion,IdPersona,IdCatalogo,IdDato,Valor,Notas,Bandera) 
				                        VALUES (@IdOrigen,@Tipo,@Posicion,@Codigo,@Descripcion,@IdPersona,@IdCatalogo,@IdDato,@Valor,@Notas,@Bandera);

			FETCH NEXT FROM TABLA INTO @Tipo,@Posicion,@Codigo,@Descripcion,@IdPersona,@IdCatalogo,@IdDato,@Valor,@Notas,@Bandera;
		END;
	CLOSE TABLA;
	DEALLOCATE TABLA;
	SET NOCOUNT OFF;
GO


/*CatDirecciones*/
CREATE TABLE [dbo].[CatDirecciones] (
	[Id]             int IDENTITY(1,1) NOT NULL ,
	[Origen]         tinyint  NOT NULL ,
	[IdOrigen]       int      NOT NULL ,
	[Posicion]       smallint NOT NULL ,
	[Rfc]            varchar (30)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Curp]           varchar (30)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Descripcion]    varchar (80)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Consignatario]  varchar (80)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TipoIde]        int     NULL ,
	[NumeroIde]      varchar (25)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdParent]       int      NULL ,
	[EsPrincipal]    tinyint  NULL ,
	[ParaEnvio]      tinyint  NULL ,
	[ParaFactura]    tinyint  NULL ,
	[EsReferencia]   tinyint  NULL ,
	[EsAval]         tinyint  NULL ,
	[IdFiscalExt]    varchar (25)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdPaisExt]      int NULL ,
	[Direccion]      varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[NoExterior]     varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	[NoInterior]     varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	[Referencia]     varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdPais]         int NULL ,
	[Pais]           varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdEstado]       int NULL ,
	[Estado]         varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdMunicipio]    int NULL ,
	[Municipio]      varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdPoblacion]    int NULL ,
	[Poblacion]      varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IdColonia]      int NULL ,
	[Colonia]        varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Cp]             varchar (6)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Telefono]       varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Telefono2]      varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Fax]            varchar (30)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Email]          varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EmailPagos]     varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[WhatsApp]       varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ViaDeEnvio]     int      NULL ,
	[TipoDePersona]  tinyint  NULL , 
	[IdEmail]        int      NULL ,
	[RegimenFiscal]  varchar (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_catDirecciones PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_catDirecciones ON [dbo].[CatDirecciones] (Origen,IdOrigen,Posicion)
GO
CREATE NONCLUSTERED INDEX IX_catDirecciones2 ON [dbo].[CatDirecciones] (TipoDePersona,Direccion)
GO
CREATE NONCLUSTERED INDEX IX_catDirecciones3 ON [dbo].[CatDirecciones] (TipoDePersona,Telefono)
GO

/*ExpedientesCuerpo*/
CREATE TABLE [dbo].[ExpedientesCuerpo] (
	[Id]              int IDENTITY(1,1) NOT NULL ,
	[Folio]           varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Fecha]           datetime NULL ,
	[Hora]            datetime NULL ,
	[Cliente]         int      NULL ,
	[TipoCliente]     int      NULL ,
	[Auditor]         int      NULL ,
	[TipoAuditoria]   int      NULL ,
	[Descripcion]     varchar (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FechaAceptacion] datetime NULL ,
	[FechaInicio]     datetime NULL ,
	[Vencimiento]     datetime NULL ,
	[FechaCierre]     datetime NULL ,
	[FechaDictamen]   datetime NULL ,
	[FolioDictamen]   varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[NumRecomendaciones]   smallint NULL ,
	[NumHallazgos]    smallint NULL ,
	[EstatusInforme]  int      NULL ,
	[PeriodoAuditar]  varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Conclusion]      int      NULL ,
	[Estatus]         int      NULL ,
	[Usuario]         varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[NumEntregados]   smallint NULL ,
	[NumPendientes]   smallint NULL ,
	[UsuarioMod]      varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FechaMod]        datetime NULL ,
	[Terminada]       tinyint  NULL ,
	CONSTRAINT PK_ExpedientesCuerpo PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_ExpedientesCuerpo ON [dbo].[ExpedientesCuerpo] (Folio)
GO

/*ExpedientesDetalle*/
CREATE TABLE [dbo].[ExpedientesDetalle] (
	[Id]           int IDENTITY(1,1) NOT NULL ,
	[IdOrigen]     int       NOT NULL ,
	[Tipo]         tinyint   NOT NULL ,
	[Posicion]     int       NOT NULL ,
	[Codigo]       int       NULL ,
	[EsRequerido]  tinyint   NULL ,
	[Dato]         int       NULL ,
	[Persona]      int       NULL ,
	[Plantilla]    int       NULL ,
	[Valor]        money     NULL ,
    [Comentarios]  nvarchar(max)  NULL ,
    [Notas]        nvarchar(max)  NULL ,
	[Estatus]      int       NULL ,
	[FechaEstatus] datetime  NULL ,
	[IdArchivo]    int       NULL ,
	[Fecha]        datetime  NULL ,
	[Usuario]      varchar  (25)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FechaMod]     datetime  NULL ,
	[UsuarioMod]   varchar  (25)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[NumReq]       int    NULL ,
	[NumAdd]       int    NULL ,
	CONSTRAINT PK_ExpedientesDetalle PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_ExpedientesDetalle ON [dbo].[ExpedientesDetalle] (IdOrigen,Tipo,Posicion)
GO

/*ExpedientesHallazgo*/
CREATE TABLE [dbo].[ExpedientesHallazgo] (
	[Id]              int IDENTITY(1,1) NOT NULL ,
	[IdOrigen]        int      NOT NULL ,
	[Folio]           int      NOT NULL ,
	[Fecha]           datetime NULL ,
	[Hora]            datetime NULL ,
	[Tipo]            int      NULL ,
	[Nivel]           int      NULL ,
	[Persona]         int      NULL ,
    [Descripcion]     nvarchar(max)  NULL ,
	[Area]            int      NULL ,
	[FechaCompromiso] datetime NULL ,
	[Estatus]         int      NULL ,
	[Usuario]         varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UsuarioMod]      varchar (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FechaMod]        datetime NULL ,
	CONSTRAINT PK_ExpedientesHallazgo PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_ExpedientesHallazgo ON [dbo].[ExpedientesHallazgo] (IdOrigen,Folio)
GO

/*CatSistemas*/
CREATE TABLE [dbo].[CatSistemas] (
	[Id]            int IDENTITY(1,1) NOT NULL ,
	[Codigo]        int       NOT NULL ,
	[Descripcion]   varchar  (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Desarrollador] varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Lenguaje]      varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Plataforma]    varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EnRed]         tinyint  NULL ,
	[EnSucursales]  tinyint  NULL ,
	CONSTRAINT PK_CatSistemas PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX IX_CatSistemas ON [dbo].[CatSistemas] (Codigo)
GO

/*CatSistemasDetalle*/
CREATE TABLE [dbo].[CatSistemasDetalle] (
	[Id]            int IDENTITY(1,1) NOT NULL ,
	[IdOrigen]      int       NOT NULL ,
	[Posicion]      smallint  NOT NULL ,
	[Descripcion]   varchar  (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Estatus]       varchar  (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_CatSistemasDetalle PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_CatSistemasDetalle] ON [dbo].[CatSistemasDetalle] (IdOrigen,Posicion)
GO

/*CatCuentasBancarias*/
CREATE TABLE [dbo].[CatCuentasBancarias] (
	[Id]                  int IDENTITY (1, 1) NOT NULL ,
	[Origen]              tinyint  NOT NULL ,
	[IdOrigen]            int      NOT NULL ,
	[Descripcion]         varchar (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Moneda]              smallint NULL ,
	[Banco]               int      NULL ,
	[NoCuenta]            varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[NoSuc]               varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Clabe]               varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[NoTarjeta]           varchar (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Sucursal]            smallint  NULL ,
	[FechaAlta]           datetime NULL ,
	[Activa]              tinyint  NULL ,
	[ManejaChequera]      tinyint  NULL ,
	[EsCaja]              tinyint  NULL ,
	[EsConcentradora]     tinyint  NULL ,
	[FolioCheque]         int      NULL ,
	[FechaDeCorte]        datetime NULL ,
	[Depositos]           money    NULL ,
	[Retiros]             money    NULL ,
	[Saldo]               money    NULL ,
	[DepositosEnTransito] money    NULL ,
	[RetirosEnTransito]   money    NULL ,
	[SaldoFinal]          money    NULL ,
	[EsPred]              tinyint  NULL ,
	CONSTRAINT PK_catCuentasBancarias PRIMARY KEY CLUSTERED (Id)
)
GO
CREATE NONCLUSTERED INDEX [IX_catCuentasBancarias] ON [dbo].[CatCuentasBancarias] (Origen,IdOrigen)
GO

/*CatDocumentosModelo*/
CREATE TABLE [dbo].[CatDocumentosModelo] (
	[Codigo]        smallint NOT NULL ,
	[Descripcion]   varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TipoAuditoria] int NULL ,
	[TipoEntidad]   int NULL ,
	[OrigenDatos]   tinyint NULL ,
	[Activo]        tinyint NULL ,
	CONSTRAINT PK_CatDocumentosModelo PRIMARY KEY CLUSTERED (Codigo)
)
GO

/*CatDocumentosTokens*/
CREATE TABLE [dbo].[CatDocumentosTokens] (
	[Codigo]        varchar (40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Descripcion]   varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TipoDato]      int NULL ,
	[Formato]       varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT PK_CatDocumentosTokens PRIMARY KEY CLUSTERED (Codigo)
)
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteNombre','Nombre del cliente',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteDireccion','Direccion del cliente',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteGiro','Giro de la empresa',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteCorto','Nombre corto del cliente',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteTipo','Tipo de entidad financiera del cliente',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteDatosGenerales','Tabla con datos generales del cliente',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteSistema','Tabla con datos de sistema utilizado por cliente',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteTablaCCC','Lista de personas que integran el comité de comunicación y control',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteSistemaNombre','Tabla con datos de sistema utilizado por cliente',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteProductos','Lista de productos del cliente',2,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ClienteFuncionarios','Tabla con funcionarios del cliente',1,'')
GO

INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaAuditor','Auditor encargado',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaPropuesta','Fecha envio de propuesta',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaFecha','Fecha de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaFolio','Folio de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaTipo','Tipo de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaEstatus','Estatus de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaIncio','Fecha de inicio de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaAceptacion','Fecha de aceptacion de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaCierre','Fecha de cierre de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaFechaDictamen','Fecha dictamen de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaFolioDictamen','Folio dictamen de auditoria',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaNumeroRecomendaciones','Numero de recomendaciones',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaNumeroHallazgos','Numero de hallazgos',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaPeriodo','Periodo a auditar',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaEstatusInforme','Estatus de informe',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaConclusion','Conclusion informe',0,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AuditoriaMinutasTemas','Lista de temas de minutas por auditoria',2,'')
GO

INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('HallazgosLista','Lista de hallazgos',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('OperacionesInusualesLista','Lista de operaciones inusuales',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('OperacionesRelevantesLista','Lista de operaciones relevantes',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('OficiosLista','Lista de oficios',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('SucursalesLista','Lista de sucursales',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AccionistasLista','Lista de accionistas',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('ConsejoLista','Lista consejo de adiministración',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('MinutasLista','Lista de minutas',1,'')
GO
INSERT INTO [dbo].[CatDocumentosTokens] (Codigo,Descripcion,TipoDato,Formato) VALUES('AcusesLista','Lista de acuses',1,'')
GO

