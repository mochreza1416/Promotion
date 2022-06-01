# Getting Started with ASP.NET CORE 6.0
## CREATE Database
```bash
CREATE Database promotionDB
```
## CREATE TABLE
```bash
DROP TABLE promotions
CREATE TABLE promotions (
	promo_id VARCHAR (50) NOT NULL PRIMARY KEY,
	promo_type_id VARCHAR (50) NULL,
	value_type_id VARCHAR (50) NULL,
	amount DECIMAL(18,2) NULL,
	item_id VARCHAR (50) NULL,
	promo_code VARCHAR (30) NULL,
	promo_description  VARCHAR (30) NULL,
	startdate DATETIME NULL,
	enddate DATETIME NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)

DROP TABLE stores
CREATE TABLE stores (
	store_id VARCHAR (50) NOT NULL PRIMARY KEY,
	store_name  VARCHAR (50) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)

DROP TABLE promo_type
CREATE TABLE promo_type (
	promo_type_id VARCHAR (50) NOT NULL PRIMARY KEY,
	promo_type_name  VARCHAR (50) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)

DROP TABLE value_type
CREATE TABLE value_type (
	value_type_id VARCHAR (50) NOT NULL PRIMARY KEY,
	value_type_name  VARCHAR (50) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)

DROP TABLE items
CREATE TABLE items (
	value_type_id VARCHAR (50) NOT NULL PRIMARY KEY,
	filename_item VARCHAR (50) NOT NULL,
	url_item VARCHAR (50) NOT NULL
)

DROP TABLE stores
CREATE TABLE stores (
	store_id VARCHAR (50) NOT NULL PRIMARY KEY,
	store_name  VARCHAR (50) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)

DROP TABLE promo_stores
CREATE TABLE promo_stores (
	promo_store_id VARCHAR (50) NOT NULL PRIMARY KEY,
	promo_code  VARCHAR (50) NOT NULL,
	store_id VARCHAR (50) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)
```
