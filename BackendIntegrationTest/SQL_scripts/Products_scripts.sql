
INSERT INTO Category (name, description) 
VALUES ('Clothing', 'Clothing (also known as clothes, apparel, and attire) are items worn on the body. Typically, clothing is made of fabrics or textiles, but over time it has included garments made from animal skin and other thin sheets of materials and natural products found in the environment, put together. ')

INSERT INTO Category (name, description) 
VALUES ('Accessories', 'a thing which can be added to something else in order to make it more useful, versatile, or attractive.')

INSERT INTO Category (name, description) 
VALUES ('School Supplies', 'School supply means an item commonly used by a student in a course of study')

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Green T-Shirt', 'UCN t-shirt in a more fitted version than the classic t-shirt from the same brand. UCN produces 100% organic and certified cotton in a socially responsible factory that only uses wind energy.
', 100, 0, 'Clothing');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Red T-Shirt', 'UCN t-shirt in a more fitted version than the classic t-shirt from the same brand. UCN produces 100% organic and certified cotton in a socially responsible factory that only uses wind energy.
', 100, 0, 'Clothing');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Orange T-Shirt', 'UCN t-shirt in a more fitted version than the classic t-shirt from the same brand. UCN produces 100% organic and certified cotton in a socially responsible factory that only uses wind energy.
', 100, 0, 'Clothing');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Blue T-Shirt', 'UCN t-shirt in a more fitted version than the classic t-shirt from the same brand. UCN produces 100% organic and certified cotton in a socially responsible factory that only uses wind energy.
', 100, 0, 'Clothing');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Black Cap', 'a kind of soft, flat hat, typically with a peak.
', 120, 0, 'Clothing');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Blue Cap', 'a kind of soft, flat hat, typically with a peak.
', 120, 0, 'Clothing');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Pen', 'a kind of soft, flat hat, typically with a peak.
', 20, 0, 'School Supplies');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Coffe Bottle', 'Every real programmer need one Coffe Bottle.
', 75, 0, 'Accessories');

INSERT INTO Product (name, description, price, discount, category)
VALUES ('Coffe Bottle OG', 'This Coffe Bottle is only for the real OG programmers that study at the UCN.
', 90, 0, 'Accessories');

INSERT INTO Sizes (size) VALUES ('S')
INSERT INTO Sizes (size) VALUES ('M')
INSERT INTO Sizes (size) VALUES ('L')
INSERT INTO Sizes (size) VALUES ('XL')
INSERT INTO Sizes (size) VALUES ('O/S')

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (1, 1, 0)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (1, 2, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (1, 3, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (1, 4, 99)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (2, 1, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (2, 2, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (2, 3, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (2, 4, 0)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (3, 1, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (3, 2, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (3, 3, 0)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (3, 4, 0)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (4, 1, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (4, 2, 99)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (4, 3, 0)
INSERT INTO ProductStock (product_id, size_id, stock) VALUES (4, 4, 99)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (5, 5, 99)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (6, 5, 99)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (7, 5, 99)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (8, 5, 99)

INSERT INTO ProductStock (product_id, size_id, stock) VALUES (9, 5, 0)

