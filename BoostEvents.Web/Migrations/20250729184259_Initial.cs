using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoostEvents.Web.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    amount_off = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    is_percentage = table.Column<bool>(type: "boolean", nullable: false),
                    is_stackable = table.Column<bool>(type: "boolean", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflow_submissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: true),
                    submitted_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    submitted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    notes = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_submissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "api_keys",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    hashed_key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    scope_json = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expires_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_revoked = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_keys", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_keys__tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_businesses", x => x.id);
                    table.ForeignKey(
                        name: "fk_businesses__tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_ai_usages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    period_start_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    period_end_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tokens_used = table.Column<int>(type: "integer", nullable: false),
                    images_generated = table.Column<int>(type: "integer", nullable: false),
                    feature_breakdown_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant_ai_usages", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenant_ai_usages_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plan_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    max_ai_tokens_per_month = table.Column<int>(type: "integer", nullable: false),
                    allow_image_generation = table.Column<bool>(type: "boolean", nullable: false),
                    allow_api_access = table.Column<bool>(type: "boolean", nullable: false),
                    allow_workflow_automation = table.Column<bool>(type: "boolean", nullable: false),
                    plan_start_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    plan_end_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant_plans", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenant_plans_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_step_answers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_submission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_field_id = table.Column<Guid>(type: "uuid", nullable: true),
                    answer_value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    answered_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_step_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflow_step_answers__workflow_submissions_workflow_submissi~",
                        column: x => x.workflow_submission_id,
                        principalTable: "workflow_submissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ai_logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prompt = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    tokens_used = table.Column<int>(type: "integer", nullable: false),
                    result_preview = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ai_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_ai_logs__businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "audit_logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    metadata_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    business_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_audit_logs__businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "business_instances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    start_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_instances", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_instances_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    external_reference_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    metadata_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_business_instances_business_instance_id",
                        column: x => x.business_instance_id,
                        principalTable: "business_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "device_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_type = table.Column<int>(type: "integer", nullable: false),
                    serial_number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    settings_encrypted_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    pairing_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    zone_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paired_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_device_tokens_business_instances_business_instance_id",
                        column: x => x.business_instance_id,
                        principalTable: "business_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_template",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_template", x => x.id);
                    table.ForeignKey(
                        name: "fk_form_template_business_instances_business_instance_id",
                        column: x => x.business_instance_id,
                        principalTable: "business_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(750)", maxLength: 750, nullable: true),
                    is_visible_to_public = table.Column<bool>(type: "boolean", nullable: false),
                    max_quantity = table.Column<int>(type: "integer", nullable: true),
                    inventory_count = table.Column<int>(type: "integer", nullable: true),
                    category = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sale_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_sale_items_business_instances_business_instance_id",
                        column: x => x.business_instance_id,
                        principalTable: "business_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflows",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflows", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflows_business_instances_business_instance_id",
                        column: x => x.business_instance_id,
                        principalTable: "business_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_field_templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    help_text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    field_type = table.Column<int>(type: "integer", nullable: false),
                    options_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_field_templates", x => x.id);
                    table.ForeignKey(
                        name: "fk_form_field_templates__form_template_form_template_id",
                        column: x => x.form_template_id,
                        principalTable: "form_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discount_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sale_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    discount_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_single_use = table.Column<bool>(type: "boolean", nullable: false),
                    max_uses = table.Column<int>(type: "integer", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount_code", x => x.id);
                    table.ForeignKey(
                        name: "fk_discount_code__sale_items_sale_item_id",
                        column: x => x.sale_item_id,
                        principalTable: "sale_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_discount_code_discounts_discount_id",
                        column: x => x.discount_id,
                        principalTable: "discounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_item_price_tiers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sale_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    start_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sale_item_price_tiers", x => x.id);
                    table.ForeignKey(
                        name: "fk_sale_item_price_tiers_sale_items_sale_item_id",
                        column: x => x.sale_item_id,
                        principalTable: "sale_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_steps",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_steps", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflow_steps_workflows_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_fields",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    field_type = table.Column<int>(type: "integer", nullable: false),
                    options_json = table.Column<string>(type: "text", nullable: true),
                    template_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_fields", x => x.id);
                    table.ForeignKey(
                        name: "fk_form_fields__form_field_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "form_field_templates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_form_fields__workflow_steps_workflow_step_id",
                        column: x => x.workflow_step_id,
                        principalTable: "workflow_steps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_step_rules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workflow_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    condition_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    action_json = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    created_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_step_rules", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflow_step_rules_workflow_steps_workflow_step_id",
                        column: x => x.workflow_step_id,
                        principalTable: "workflow_steps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ai_logs_business_id",
                table: "ai_logs",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_tenant_id",
                table: "api_keys",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_business_id",
                table: "audit_logs",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_instances_business_id",
                table: "business_instances",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_tenant_id",
                table: "businesses",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_business_instance_id",
                table: "clients",
                column: "business_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_device_tokens_business_instance_id",
                table: "device_tokens",
                column: "business_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_code_discount_id",
                table: "discount_code",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_code_sale_item_id",
                table: "discount_code",
                column: "sale_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_field_templates_form_template_id",
                table: "form_field_templates",
                column: "form_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_fields_template_id",
                table: "form_fields",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_fields_workflow_step_id",
                table: "form_fields",
                column: "workflow_step_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_template_business_instance_id",
                table: "form_template",
                column: "business_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_item_price_tiers_sale_item_id",
                table: "sale_item_price_tiers",
                column: "sale_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_items_business_instance_id",
                table: "sale_items",
                column: "business_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_ai_usages_tenant_id",
                table: "tenant_ai_usages",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_tenant_plans_tenant_id",
                table: "tenant_plans",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_step_answers_workflow_submission_id",
                table: "workflow_step_answers",
                column: "workflow_submission_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_step_rules_workflow_step_id",
                table: "workflow_step_rules",
                column: "workflow_step_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_steps_workflow_id",
                table: "workflow_steps",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflows_business_instance_id",
                table: "workflows",
                column: "business_instance_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ai_logs");

            migrationBuilder.DropTable(
                name: "api_keys");

            migrationBuilder.DropTable(
                name: "audit_logs");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "device_tokens");

            migrationBuilder.DropTable(
                name: "discount_code");

            migrationBuilder.DropTable(
                name: "form_fields");

            migrationBuilder.DropTable(
                name: "sale_item_price_tiers");

            migrationBuilder.DropTable(
                name: "tenant_ai_usages");

            migrationBuilder.DropTable(
                name: "tenant_plans");

            migrationBuilder.DropTable(
                name: "workflow_step_answers");

            migrationBuilder.DropTable(
                name: "workflow_step_rules");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "form_field_templates");

            migrationBuilder.DropTable(
                name: "sale_items");

            migrationBuilder.DropTable(
                name: "workflow_submissions");

            migrationBuilder.DropTable(
                name: "workflow_steps");

            migrationBuilder.DropTable(
                name: "form_template");

            migrationBuilder.DropTable(
                name: "workflows");

            migrationBuilder.DropTable(
                name: "business_instances");

            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropTable(
                name: "tenants");
        }
    }
}
