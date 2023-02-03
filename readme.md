# GrapDB - Neo4j - Cypher - NetCoreApi
	Cấu hình
	1. Cài đặt Neo4j Desktop
		https://neo4j.com/download/
	2. Tạo database local
		neo4j://localhost:7687 (neo4j/neo4j/OCB@1234)
	3. Tạo data - vd tạo từ file csv
		- Tạo node khách hàng - đủ thông tin
		LOAD CSV FROM "file:///customer-phone.csv" as a 
		create (c: Customer {CustomerId: a[0],CustomerCode: a[1], CustomerName: a[2], Gender: a[4], Phone: trim(a[5])})

		- Tạo node số điện thoại
		LOAD CSV FROM "file:///phone.csv" as a 
		create (c: Phone {Phone: trim(a[0])})

		- Tạo quan hệ khách hàng - số điện thoại
		MATCH (c:Customer)
		MATCH (p: Phone)
		WHERE p.Phone = c.Phone
		CREATE (c) - [rel:HAS_PHONE] -> (p)

		- Query mẫu - lấy số điện thoại có 3 khách hàng dùng chung
		match (c:Customer)-[r]->(p:Phone)
		with p, count(r) as count_c
		where count_c > 3
		match (p)<-[]-(c:Customer)
		return p,c

# Request Api
	GET: 
		- https://localhost:7278/Person/SearchCarRelation?name1=Mike&name2=Bob
	POST:
		https://localhost:7278/query/cypher
		- Danh sách toàn bộ customer
		{
			"Query": "MATCH (c:Customer) return c {name: c.CustomerName, Phone: c.Phone}",
			"ReturnObject": "c"
		}

		- Danh sách số điện thoại có trên 3 người dùng chung
		{
			"Query": "match (c:Customer)-[r]->(p:Phone) with p, count(r) as count_c where count_c > 3 match (p)<-[]-(c:Customer) with p, collect(c{.CustomerName,.Gender}) as listCustomer return p{Phone: p.Phone,ListCustomer: listCustomer}",
			"ReturnObject": "p"
		}