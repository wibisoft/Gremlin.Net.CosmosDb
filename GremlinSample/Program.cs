namespace GremlinSample
{
	using Gremlin.Net.CosmosDb;
	using Schema;
	using Newtonsoft.Json;
	using System;
	using System.Threading.Tasks;
	using Gremlin.Net.CosmosDb.Structure;

	internal class Program
	{
		internal static async Task Main()
		{
			using (GraphClient graphClient = new GraphClient("archgraph1.gremlin.cosmos.azure.com", "organization", "test", "rtiwtIxpuSPMxXEy8GEvHHYzrOHg931OdXmTVzlo4Jjo2eeG5d2FOuWpxSitkMGXz3YcOEg8zKBklpViUR2pNg=="))
			{
				IGraphTraversalSource g = graphClient.CreateTraversalSource();

				//add vertices/edges using strongly-typed objects
				PersonVertex personV = new PersonVertex
				{
					Ages = new[] { 4, 6, 23 },
					Id = "person-12345",
					Name = "my person"
				};
				ProductVertex productV = new ProductVertex
				{
					Id = "product-12345",
					Name = "my product"
				};
				PersonPurchasedProductEdge purchasedE = new PersonPurchasedProductEdge
				{
					Id = "person-12345_purchased_product-12345",
					Name = "my person"
				};

				ISchemaBoundTraversal<object, PersonPurchasedProductEdge> test = g
					.AddV(personV).As("person")
					.AddV(productV).As("product")
					.AddE(purchasedE).From("person").To("product");

				Console.WriteLine(test.ToGremlinQuery());

				await graphClient.ExecuteAsync(test);

				//traverse vertices/edges with strongly-typed objects
				ISchemaBoundTraversal<object, PersonVertex> query = g
					.V("1")
						.Cast<PersonVertex>()
					.Out(_ => _.Purchases)
					.InE(_ => _.People)
					.OutV()
					.Property(_ => _.Name, "test")
					.Property(_ => _.Ages, new[] { 5, 6 })
					.Property(_ => _.Ages, 7);

				Console.WriteLine(query.ToGremlinQuery());
				GraphResult<PersonVertex> response2 = await graphClient.QueryAsync(query);

				Console.WriteLine();
				Console.WriteLine("Response status:");

				Console.WriteLine($"Code: {response2.StatusCode}");
				Console.WriteLine($"RU Cost: {response2.TotalRequestCharge}");

				Console.WriteLine();
				Console.WriteLine("Response:");
				foreach (PersonVertex result in response2)
				{
					string json = JsonConvert.SerializeObject(result, Formatting.Indented);

					Console.WriteLine(json);
				}
			}

			Console.WriteLine();
			Console.WriteLine("All done...");
			Console.ReadKey();
		}
	}
}