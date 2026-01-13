using FluentMigrator;

namespace Itmo.CSharpMicroservices.Lab4.Persistance.Migrations;

[Migration(000000)]
public class CreateProductOrderSchema : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TYPE order_state AS ENUM ('created', 'processing', 'completed', 'cancelled',  'indelivery', 'delivered', 'packing', 'packed', 'approved', 'approval');
        ");

        Create.Table("products")
            .WithColumn("product_id").AsInt64().PrimaryKey().Identity()
            .WithColumn("product_name").AsString().NotNullable()
            .WithColumn("product_price").AsCustom("money").NotNullable()
            .WithColumn("product_deleted").AsBoolean().NotNullable();

        Create.Table("orders")
            .WithColumn("order_id").AsInt64().PrimaryKey().Identity()
            .WithColumn("order_state").AsCustom("order_state").NotNullable()
            .WithColumn("order_created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("order_created_by").AsString().NotNullable();

        Create.Table("order_items")
            .WithColumn("order_item_id").AsInt64().PrimaryKey().Identity()
            .WithColumn("order_id").AsInt64().NotNullable().ForeignKey("orders", "order_id")
            .WithColumn("product_id").AsInt64().NotNullable().ForeignKey("products", "product_id")
            .WithColumn("order_item_quantity").AsInt32().NotNullable()
            .WithColumn("order_item_deleted").AsBoolean().NotNullable();

        Execute.Sql(@"
            CREATE TYPE order_history_item_kind AS ENUM ('created', 'itemadded', 'itemremoved', 'statechanged');
        ");

        Create.Table("order_history")
            .WithColumn("order_history_item_id").AsInt64().PrimaryKey().Identity()
            .WithColumn("order_id").AsInt64().NotNullable().ForeignKey("orders", "order_id")
            .WithColumn("order_history_item_created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("order_history_item_kind").AsCustom("order_history_item_kind").NotNullable()
            .WithColumn("order_history_item_payload").AsCustom("jsonb").NotNullable();
    }

    public override void Down()
    {
        Delete.Table("order_history");
        Execute.Sql("DROP TYPE order_history_item_kind;");

        Delete.Table("order_items");
        Delete.Table("orders");
        Delete.Table("products");
        Execute.Sql("DROP TYPE order_state;");
    }
}
