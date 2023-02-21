using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EF.Migrations.IFDb
{
    public partial class NewUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529",
                column: "ConcurrencyStamp",
                value: "e4c3514f-ca6d-48a1-a36f-f34337a66837");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dbf2938-3dbe-472d-8ee9-3596f95e7931",
                column: "ConcurrencyStamp",
                value: "22b64c74-4728-481b-9dfb-e55e5240bd3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "002942bd-9cac-44b4-896e-bbab0a96189f", "AQAAAAEAACcQAAAAEKo9YqKkuAy8HCMa2tst7vdbuTKdfomDXCH2cpoSZRqAsdP4SPSdrO7wuGy/QgYM2A==", "736f0c69-9611-4a30-92c9-8d2fd78105e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d90704e7-510d-4df0-a9ff-aaa8ed58f1ae",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec345916-df25-421b-a844-6dba2116547b", "AQAAAAEAACcQAAAAEK1R1hoGoruTI9A30HMVoKlfW3Tio7XwKj0ThBspfE4zY54rCIVyyzlVvmfHxy0/PA==", "165dd393-81f9-4eca-81be-cb76fade3378" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7b445865-a24d-4543-a6c6-9443d048cd1c", 0, "9c0a2815-2101-4c3b-be37-151b0a62e7bc", "rm.vandergaag@student.avans.nl", false, false, null, "RM.VANDERGAAG@STUDENT.AVANS.NL", "RM.VANDERGAAG@STUDENT.AVANS.NL", "AQAAAAEAACcQAAAAEO1usYIl09ojjX5yOELJt3ai+vpnGbdwYTpwpq6ZmEyQPCSb2z/EZHzh6bUH5AYDuQ==", null, false, "be3b1a1f-1637-4306-a427-cc03ca1db55d", false, "rm.vandergaag@student.avans.nl" },
                    { "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd", 0, "2ea78c09-8ac6-469d-afd5-6c831876b71b", "gerard@avans.nl", false, false, null, "GERARD@AVANS.NL", "GERARD@AVANS.NL", "AQAAAAEAACcQAAAAEOcv51N2I+52S5Dvkm89eK7RAM5vwXLRg+PjGldVa89yDbtR3LWiJOi2BgtdoNNiqQ==", null, false, "8d53dec4-7e49-42f5-8c68-5f7d749d8766", false, "gerard@avans.nl" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529", "7b445865-a24d-4543-a6c6-9443d048cd1c" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8dbf2938-3dbe-472d-8ee9-3596f95e7931", "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529", "7b445865-a24d-4543-a6c6-9443d048cd1c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8dbf2938-3dbe-472d-8ee9-3596f95e7931", "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b445865-a24d-4543-a6c6-9443d048cd1c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b10704e7-510d-4df0-a9ff-aaa8ed58f1cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c7fc7d2-87b3-4edd-bb68-4f1d69fc7529",
                column: "ConcurrencyStamp",
                value: "2b9edb5f-1b93-4316-b311-b6d74bc2ed86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dbf2938-3dbe-472d-8ee9-3596f95e7931",
                column: "ConcurrencyStamp",
                value: "d0b8f139-fb1b-4469-ada8-527dc7a6b1f4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcc84ee2-661f-44cf-9a46-25dc3a12f15f", "AQAAAAEAACcQAAAAELZvd/PDx+zFe1QWsOPW2KzfgzwYkbO/+ib1bnCVuvzXrcvBy70Ob4cVKgSml7X45g==", "3482d75c-e81f-4fab-aca3-2afc301a126f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d90704e7-510d-4df0-a9ff-aaa8ed58f1ae",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d128e02e-13b7-444d-994f-25569cebb4e6", "AQAAAAEAACcQAAAAEMx2CCwf3FTjzsLxbwD+9hL2CZLgmGPTy68HEH4T0+5znPvyz4Z3sTEJjU7+rwpalg==", "f69a8662-7de4-4a86-8261-6589d0fdd028" });
        }
    }
}
