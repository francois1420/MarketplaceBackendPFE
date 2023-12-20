


DROP TABLE dbo.item_user_views;
DROP TABLE dbo.cart_lines;
DROP TABLE dbo.item_images;
DROP TABLE dbo.items;
DROP TABLE dbo.base_items;
DROP TABLE dbo.categories;
DROP TABLE dbo.notifications;
DROP TABLE dbo.carts;
DROP TABLE dbo.addresses;
DROP TABLE dbo.users;




CREATE TABLE dbo.users (
	id_user			INT IDENTITY(1,1)	PRIMARY KEY,
	first_name		VARCHAR(255)		NOT NULL,
	last_name		VARCHAR(255)		NOT NULL,
	email			VARCHAR(255)		NOT NULL UNIQUE,
	password		CHAR(60)			NOT NULL,
	phone_number	VARCHAR(255)		NULL,
	role			VARCHAR(60)			NOT NULL CHECK (role IN ('member', 'admin')) DEFAULT 'member',
	created_date	DATETIME			NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.addresses (
	id_address			INT IDENTITY(1,1)	PRIMARY KEY,
	street_number		VARCHAR(255)		NOT NULL,
	appartment_number	VARCHAR(255)		NULL,
	street_name			VARCHAR(255)		NOT NULL,
	city				VARCHAR(255)		NOT NULL,
	state				VARCHAR(255)		NOT NULL,
	zip_code			VARCHAR(255)		NOT NULL,
	country				VARCHAR(255)		NOT NULL,
	id_user				INT					NOT NULL FOREIGN KEY REFERENCES dbo.users(id_user)

);
	
CREATE TABLE dbo.carts (
	id_cart			INT IDENTITY(1,1)	PRIMARY KEY,
	id_user			INT					NOT NULL FOREIGN KEY REFERENCES dbo.users(id_user),
	is_bought		BIT					NOT NULL DEFAULT 0,
	bought_date		DATETIME			NULL,
	created_date	DATETIME			NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.notifications (
	id_notification		INT IDENTITY(1,1)	PRIMARY KEY,
	id_user				INT					NOT NULL FOREIGN KEY REFERENCES dbo.users(id_user),
	id_cart				INT					NULL FOREIGN KEY REFERENCES dbo.carts(id_cart),
	notification_text	VARCHAR(255)		NOT NULL,
	was_seen			BIT					NOT NULL DEFAULT 0,
	created_date		DATETIME			NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.categories (
	id_category		INT IDENTITY(1,1)	PRIMARY KEY,
	name			VARCHAR(255)		NOT NULL,
	url_image		VARCHAR(512)		NOT NULL,
	created_date	DATETIME			NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.base_items (
	id_base_item	INT IDENTITY(1,1)	PRIMARY KEY,
	name			VARCHAR(255)		NOT NULL,
	description		VARCHAR(512)		NOT NULL,
	id_category		INT					NOT NULL FOREIGN KEY REFERENCES dbo.categories(id_category),
	created_date	DATETIME			NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.items (
	id_item			INT IDENTITY(1,1)	PRIMARY KEY,
	id_base_item	INT					NOT NULL FOREIGN KEY REFERENCES dbo.base_items(id_base_item),
	size			VARCHAR(255)		NULL,
	color			VARCHAR(255)		NULL,
	stock			int					NOT NULL,
	price			SMALLMONEY			NOT NULL,
	created_date	DATETIME			NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.item_images (
	id_image		INT IDENTITY(1,1)	PRIMARY KEY,
	id_item			INT					NOT NULL FOREIGN KEY REFERENCES dbo.items(id_item),
	is_main			BIT					NOT NULL DEFAULT 0,
	url_image		VARCHAR(512)		NOT NULL
);

CREATE TABLE dbo.cart_lines (
	id_cart			INT			NOT NULL FOREIGN KEY REFERENCES dbo.carts(id_cart),
	id_item			INT			NOT NULL FOREIGN KEY REFERENCES dbo.items(id_item),
	quantity		INT			NOT NULL DEFAULT 1 CHECK (quantity > 0),
	is_package_sent	BIT			NOT NULL DEFAULT 0,
	created_date	DATETIME	NOT NULL DEFAULT GETDATE(),
	PRIMARY KEY (id_cart, id_item)
);

CREATE TABLE dbo.item_user_views (
	id_user			INT			NOT NULL FOREIGN KEY REFERENCES dbo.users(id_user),
	id_item			INT			NOT NULL FOREIGN KEY REFERENCES dbo.items(id_item),
	date_view		DATETIME	NOT NULL DEFAULT GETDATE(),
	PRIMARY KEY (id_user, id_item)
);