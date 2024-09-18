using BLL;
using DAL;
using FluentValidation;
using M7BookAPI.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

///////////////////////////
/// IOC Configuration !
///////////////////////////

// Add Controllers in IOC.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilterAttribute));
});


//Note: We can use fluentautovalidation for the validation of the DTOs in the API
//But if we use the automatic pipeline of the FluentValidation, we can't use ASYNC Rules in validators
//So we will use the manual validation in the API controllers
//if we want to use the automatic pipeline of the FluentValidation, we need to use the FluentAutoValidation in [using FluentValidation.AspNetCore;]
//Line to add :
//builder.Services.AddFluentValidationAutoValidation();

//Add the FluentValidators in the IOC
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//ADD the BLL in the IOC
builder.Services.AddBLL( (options) => {});

//ADD the DAL in the IOC
builder.Services.AddDAL((options) => {
    options.DBConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Enum.TryParse(builder.Configuration.GetValue<string>("DBType"), out DBType dbType);
    options.DBType = dbType;

});


//DOCUMENTATION SERVICES <= Next in the course
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Add documentation to the API using Swagger and Swagger UI in IOC
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
 
});



var app = builder.Build();

////////////////////////////////////////////
/////      Pipeline middleware !
//// Configure the HTTP request pipeline !!!
////////////////////////////////////////////


if (app.Environment.IsDevelopment())
{
    //Here all the middlewares that are only for development environment

    // [DOCUMENTATION] Enable middleware to serve generated Swagger as a JSON endpoint. (Development only)
    app.UseSwagger();
    app.UseSwaggerUI();
}


//We don't need this line because we don't have any HTTPS certificate
//app.UseHttpsRedirection();

//Later we will add the Authorization and Authentification in the API
//app.UseAuthorization();

//Add middleware to handle the request
app.MapControllers();

// Run the application
app.Run();

public partial class  Program {}
