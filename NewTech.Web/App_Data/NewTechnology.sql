
CREATE TABLE [dbo].[Dict](
	[Id] [varchar](50) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Remark] [varchar](2000) NULL,
	[Order] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Dict] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1001', 'Category', 'Desktop Application', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1002', 'Category', 'Web Application', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1003', 'Category', 'Mobile Application', 1);


INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1011', 'Technology', 'VB.NET', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1012', 'Technology', 'C#', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1013', 'Technology', 'ASP.NET', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1014', 'Technology', 'ASP.NET MVC', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1015', 'Technology', 'SQL Server 2005', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1016', 'Technology', 'SQL Server 2008', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1017', 'Technology', 'SQL Server 2008 R2', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1018', 'Technology', 'SQL Server 2012', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1019', 'Technology', 'SQL Server Integration Service', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1020', 'Technology', 'SQL Server Analysis Services', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1021', 'Technology', 'SQL Server Reporting Services', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1022', 'Technology', 'Microsoft Office 2010', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1023', 'Technology', 'Microsoft SharePoint Server 2007', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1024', 'Technology', 'Microsoft SharePoint Server 2010', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1025', 'Technology', 'Microsoft SharePoint Server 2013', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1026', 'Technology', 'Java', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1027', 'Technology', 'MySql', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1028', 'Technology', 'PHP', 1);


INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1101', 'Industry', '������֯', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1102', 'Industry', 'ũ������', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1103', 'Industry', 'ҽҩ����', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1104', 'Industry', '��������', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1105', 'Industry', 'ұ����', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1106', 'Industry', 'ʯ�ͻ���', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1107', 'Industry', 'ˮ��ˮ��', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1108', 'Industry', '��ͨ����', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1109', 'Industry', '��Ϣ��ҵ', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1110', 'Industry', '��е����', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1111', 'Industry', '�ṤʳƷ', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1112', 'Industry', '��װ��֯', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1113', 'Industry', 'רҵ����', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1114', 'Industry', '��ȫ����', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1115', 'Industry', '�����̻�', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1116', 'Industry', '��������', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1117', 'Industry', '�칫�Ľ�', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1118', 'Industry', '���ӵ繤', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1119', 'Industry', '�����Ʒ', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1120', 'Industry', '�Ҿ���Ʒ', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1121', 'Industry', '����ר��', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1122', 'Industry', '��װ��Ʒ', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1123', 'Industry', '������Ʒ', 1);
INSERT INTO dbo.Dict(Id, Category, Name, Status) VALUES('1124', 'Industry', '�칫�Ҿ�', 1);


