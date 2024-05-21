--Category 1
INSERT INTO Categories (Id, Name, Description)
VALUES (
    '2F6E80FE-C912-4493-867F-1E3472A80EF7',
    'Tablet', 
    'Tablet to surf website', 
);

--Category 2
INSERT INTO Categories (Id, Name, Description)
VALUES (
    'FEE6D536-00C5-479D-B37F-57D59752E8E4',
    'Phone', 
    'Phone for calling or texting', 
);
--Category 3
INSERT INTO Categories (Id, Name, Description)
VALUES (
    'A0E40DA5-F642-46DF-A4F4-A9B6B87935AD',
    'Laptop', 
    'Laptop for students and business man', 
);
--Category 4
INSERT INTO Categories (Id, Name, Description)
VALUES (
    '2544B768-B7D9-4FA1-8A60-C7F61853475B',
    'Smartwatch', 
    'Watch for tracking sports', 
);
--Category 5
INSERT INTO Categories (Id, Name, Description)
VALUES (
    'AAD50E83-7EA3-4677-BFB3-E2E838096616',
    'Player', 
    'Listen to music or watch video', 
);

--Product 1
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    '8CFA19E3-D235-4FBD-B118-2FFD17BE059F',
    'IPad Pro M4 11 inch WiFi 256GB', 
    'Most powerful Ipad', 
    1495.99, 
    8,
    '2F6E80FE-C912-4493-867F-1E3472A80EF7'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    '8B867B3D-F5B8-477B-9F28-BF391C098E4E',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716191006/EcommerceWebsite/ipad-pro-11-inch-m4-wifi-sliver-1_puqmum.jpg', 
    'EcommerceWebsite/ipad-pro-11-inch-m4-wifi-sliver-1_puqmum', 
    '8CFA19E3-D235-4FBD-B118-2FFD17BE059F'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    'CBC69508-AE79-46EE-A931-AA046837AD01',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716191673/EcommerceWebsite/ipad-pro-11-inch-m4-wifi-black-1_jysvq2.jpg',
    'EcommerceWebsite/ipad-pro-11-inch-m4-wifi-black-1_jysvq2', 
    '8CFA19E3-D235-4FBD-B118-2FFD17BE059F'
);

INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'8CFA19E3-D235-4FBD-B118-2FFD17BE059F',
    '8B867B3D-F5B8-477B-9F28-BF391C098E4E'
);

--Product 2
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    'EA616854-3B76-4DD2-9F19-9C6AE1C54A5F',
    'IPad Pro M4 13 inch WiFi 256GB', 
    'Most powerful Ipad', 
    1985.99, 
    8,
    '2F6E80FE-C912-4493-867F-1E3472A80EF7'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    '1B65A543-3111-4575-8405-E405BE74B31F',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716192847/EcommerceWebsite/ipad-pro-13-inch-m4-wifi-black-1_mpkmvj.jpg',
    'EcommerceWebsite/ipad-pro-13-inch-m4-wifi-black-1_mpkmvj', 
    'EA616854-3B76-4DD2-9F19-9C6AE1C54A5F'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    'A59B816D-BA7D-48CB-A178-F4B2D29B410A',
	'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716192847/EcommerceWebsite/ipad-pro-13-inch-m4-wifi-sliver-1_zlyxiu.jpg',
    'EcommerceWebsite/ipad-pro-13-inch-m4-wifi-sliver-1_zlyxiu', 
    'EA616854-3B76-4DD2-9F19-9C6AE1C54A5F'
);

INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'EA616854-3B76-4DD2-9F19-9C6AE1C54A5F',
    '1B65A543-3111-4575-8405-E405BE74B31F'
);

--Product 3
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    '3494FD26-8B56-4189-B3CA-ABF179D84039',
    'IPad Air 6 M2 11 inch WiFi 128GB', 
    'Ipad for students', 
    1300.99, 
    15,
    '2F6E80FE-C912-4493-867F-1E3472A80EF7'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    '1D9825E8-75C9-43BE-A2D2-D94360CF197D',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716193655/EcommerceWebsite/ipad-air-11-inch-m2-wifi-grey-1_p0yag7.jpg',
    'EcommerceWebsite/ipad-air-11-inch-m2-wifi-grey-1_p0yag7', 
    '3494FD26-8B56-4189-B3CA-ABF179D84039'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    'D5255BA9-8563-4968-B745-A1EB08B6EFB6',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716193655/EcommerceWebsite/ipad-air-11-inch-m2-wifi-blue-1_zo3hel.jpg',
    'EcommerceWebsite/ipad-air-11-inch-m2-wifi-blue-1_zo3hel', 
    '3494FD26-8B56-4189-B3CA-ABF179D84039'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    '0DC7B519-D073-46FB-AB03-A604934597D8',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716193656/EcommerceWebsite/ipad-air-11-inch-m2-wifi-kem-1_cbjuyw.jpg',
    'EcommerceWebsite/ipad-air-11-inch-m2-wifi-kem-1_cbjuyw', 
    '3494FD26-8B56-4189-B3CA-ABF179D84039'
);


INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'3494FD26-8B56-4189-B3CA-ABF179D84039',
    '0DC7B519-D073-46FB-AB03-A604934597D8'
);

--Product 4
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    'A86A4500-5353-4831-BA8A-A1E554382C90',
    'Samsung Galaxy Tab A9 4G', 
    'Ipad for students', 
    250.99, 
    7,
    '2F6E80FE-C912-4493-867F-1E3472A80EF7'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    '7A6DD83F-0EB7-4420-929F-43EDB8DC3112',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194376/EcommerceWebsite/samsung-galaxy-tab-a9-xanh-1_l5algr.jpg',
    'EcommerceWebsite/samsung-galaxy-tab-a9-xanh-1_l5algr', 
    'A86A4500-5353-4831-BA8A-A1E554382C90'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
    '21EFA180-DB8B-41AB-902A-851F9D2638A3',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194377/EcommerceWebsite/samsung-galaxy-tab-a9-bac-1-1_zim47e.jpg',
    'EcommerceWebsite/samsung-galaxy-tab-a9-bac-1-1_zim47e', 
    'A86A4500-5353-4831-BA8A-A1E554382C90'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'EE261E15-FE35-4B2F-B3FB-18C19879CFBC',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194377/EcommerceWebsite/samsung-galaxy-tab-a9-xam-1_fuzhhi.jpg',
    'EcommerceWebsite/samsung-galaxy-tab-a9-xam-1_fuzhhi', 
    'A86A4500-5353-4831-BA8A-A1E554382C90'
);

INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'A86A4500-5353-4831-BA8A-A1E554382C90',
    'EE261E15-FE35-4B2F-B3FB-18C19879CFBC'
);

--Product 5
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    '1921D564-0B41-4D14-902D-8EE3E998031D',
    'Lenovo Tab M9', 
    'Ipad for students', 
    100.99, 
    69,
    '2F6E80FE-C912-4493-867F-1E3472A80EF7'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'740EB19E-C083-4C7D-9B04-2E86697A9694',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194744/EcommerceWebsite/lenovo-tab-m9-1-1_goqgvx.jpg',
    'EcommerceWebsite/lenovo-tab-m9-1-1_goqgvx', 
    '1921D564-0B41-4D14-902D-8EE3E998031D'
);

INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'1921D564-0B41-4D14-902D-8EE3E998031D',
    '740EB19E-C083-4C7D-9B04-2E86697A9694'
);

--Product 6
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    '95A534A6-EEC6-49E0-969A-320E174E4026',
    'Xiaomi Redmi Pad SE (4GB/128GB) ', 
    'Ipad for learning', 
    123.45, 
    4,
    '2F6E80FE-C912-4493-867F-1E3472A80EF7'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'BF3229B1-8B24-44FF-83D7-3BB6B884709F',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194938/EcommerceWebsite/xiaomi-pad-se-tim-1-1_qcq84j.jpg',
    'EcommerceWebsite/xiaomi-pad-se-tim-1-1_qcq84j', 
    '95A534A6-EEC6-49E0-969A-320E174E4026'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'1D7DAF8A-4415-430C-BCA1-7DF4E0D9AB3A',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194940/EcommerceWebsite/xiaomi-pad-se-xam-1-1_cujxmd.jpg',
    'EcommerceWebsite/xiaomi-pad-se-xam-1-1_cujxmd', 
    '95A534A6-EEC6-49E0-969A-320E174E4026'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'70F07478-6ADE-4260-88E3-8015EF9A0351',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716194941/EcommerceWebsite/xiaomi-pad-se-xanh-1-1_nm01wl.jpg',
    'EcommerceWebsite/xiaomi-pad-se-xanh-1-1_nm01wl', 
    '95A534A6-EEC6-49E0-969A-320E174E4026'
);

INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'95A534A6-EEC6-49E0-969A-320E174E4026',
    'BF3229B1-8B24-44FF-83D7-3BB6B884709F'
);

--Product 7
INSERT INTO Products (Id, ProductName, Description, Price, QuantityInStock, CategoryId)
VALUES (
    'EEAAA5C9-E0AE-44F6-8E84-370BCF55DB8C',
    'IPhone 15 Pro Max 256GB', 
    'Ipad for learning', 
    320.23, 
    9,
    'FEE6D536-00C5-479D-B37F-57D59752E8E4'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'F8057CE4-2E73-41D0-ABD4-F39937B2EFB5',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716201975/EcommerceWebsite/iphone-15-pro-max-tu-nhien-1-1_ookfza.jpg',
    'EcommerceWebsite/iphone-15-pro-max-tu-nhien-1-1_ookfza', 
    'EEAAA5C9-E0AE-44F6-8E84-370BCF55DB8C'
);

INSERT INTO Images (Id, Url, PublicId, ProductId)
VALUES (
	'7C6AE194-CA85-452B-AD86-A7A8A8B87DC9',
    'https://res.cloudinary.com/dqwwmou7b/image/upload/v1716201974/EcommerceWebsite/iphone-15-pro-max-white-1-3_lzxfv7.jpg',
    'EcommerceWebsite/iphone-15-pro-max-white-1-3_lzxfv7', 
    'EEAAA5C9-E0AE-44F6-8E84-370BCF55DB8C'
);

INSERT INTO MainImages (ProductId, ImageId)
VALUES (
	'EEAAA5C9-E0AE-44F6-8E84-370BCF55DB8C',
    '7C6AE194-CA85-452B-AD86-A7A8A8B87DC9'
);