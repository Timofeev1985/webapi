using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities;
using webapi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/users", () =>
{
   TestContext context = new TestContext();
   var users = context.Users.ToList().ConvertAll(x=>new UserResponse(x));

    return users;
})
.WithName("GetUsers")
.WithOpenApi();

app.MapGet("/user/{id}", (int id) =>
{
   TestContext context = new TestContext();
   var user = context.Users.FirstOrDefault(x=>x.Id == id);
    return user;
})
.WithName("GetUser")
.WithOpenApi();

app.MapDelete("/user/{id}", (int id) =>
{
   TestContext context = new TestContext();
   var user = context.Users.FirstOrDefault(x=>x.Id == id);
   if (user != null){
    context.Users.Remove(user);
    context.SaveChanges();
    return Results.Ok("Пользователь удален!");
   }
   else{
    return Results.BadRequest("Пользователь не найден!!");
   }

})
.WithName("DeleteUser")
.WithOpenApi();

app.MapPost("/user",async ([FromForm] UserRequest request)=>{
    var user = new User(request);
    TestContext context = new TestContext();
    user.Photo = await SaveImage(request.Photo);
    context.Users.Add(user);
    context.SaveChanges();
     return Results.Ok("Пользователь добавлен!");
})
.DisableAntiforgery()
.Accepts<UserRequest>("multipart/form-data")
.WithName("CreateUser")
.WithOpenApi();

app.MapPut("/user/{id}",async (int id,[FromForm] UserRequest request)=>{
   
    TestContext context = new TestContext();
    var user = context.Users.FirstOrDefault(x=> x.Id == id);
    user.Copy(request);
    user.Photo = await SaveImage(request.Photo);
    context.SaveChanges();
    return Results.Ok("Пользователь изменен!");
})
.DisableAntiforgery()
.Accepts<UserRequest>("multipart/form-data")
.WithName("UpdateUser")
.WithOpenApi();

async Task<byte[]> SaveImage(IFormFile file){
    using (var stream = new MemoryStream()){
        file.CopyToAsync(stream).Wait();
        return stream.ToArray();
    }
}
app.UseCors(option=> option
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin=>true)
.AllowCredentials()
);
app.Run();


