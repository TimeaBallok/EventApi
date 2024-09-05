using EventAPI.Features.Common.Behaviors;
using EventAPI.Features.Common.Middleware;
using EventAPI.Features.Events;
using EventAPI.Features.Events.Create;
using EventAPI.Features.Events.Get_All;
using EventAPI.Features.Events.Update;
using EventAPI.Features.Users;
using EventAPI.Features.Users.Create;
using EventAPI.Features.Users.Update;
using EventAPI.Infrastructure.Database;
using EventAPI.Infrastructure.Minimal_API;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<EventDB>(opt => opt.UseInMemoryDatabase("EventList"));
builder.Services.AddDbContext<EventDB>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EventDbConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors();

builder.Services.AddScoped<IValidator<CreateEvent>, CreateEventValidator>();
builder.Services.AddScoped<IValidator<GetEventById>, GetEventByIdValidator>();
builder.Services.AddScoped<IValidator<UpdateEvent>, UpdateEventValidator>();
builder.Services.AddScoped<IValidator<CreateUser>, CreateUserValidator>();
builder.Services.AddScoped<IValidator<GetUserById>, GetUserByIdValidator>();
builder.Services.AddScoped<IValidator<UpdateUser>, UpdateUserValidator>();

builder.Services.AddEndpoints(typeof(Program).Assembly);
var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services
    .AddGraphQLServer()
    .AddEventAPITypes()
    .AddSorting()
    .AddFiltering();
    

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.MapEndpoints();

app.MapGraphQL("/graphql");


//app.MapGet("/events", async (EventDB db) =>
//    await db.Events.ToListAsync());

//app.MapGet("/events/{id}", async (int id, EventDB db) =>
//    await db.Events.FindAsync(id)
//        is Event evnt
//            ? Results.Ok(evnt)
//            : Results.NotFound());

//app.MapPost("/events", async (Event evnt, EventDB db, IValidator<Event> validator) =>
//{
//    var validationResult = await validator.ValidateAsync(evnt);
//    if (!validationResult.IsValid)
//    {
//        return Results.BadRequest(validationResult.Errors);
//    }
//    db.Events.Add(evnt);
//    await db.SaveChangesAsync();

//    return Results.Created($"/events/{evnt.EventId}", evnt);
//});

//app.MapPut("/events/{id}", async (int id, Event inputEvent, EventDB db, IValidator<Event> validator) =>
//{
//    var evnt = await db.Events.FindAsync(id);

//    if (evnt is null) return Results.NotFound();

//    var validationResult = await validator.ValidateAsync(inputEvent);
//    if (!validationResult.IsValid)
//    {
//        return Results.BadRequest(validationResult.Errors);
//    }

//    evnt.Title = inputEvent.Title;
//    evnt.Location = inputEvent.Location;
//    evnt.Description = inputEvent.Description;
//    evnt.Price = inputEvent.Price;

//    await db.SaveChangesAsync();

//    return Results.Ok(evnt);
//});

//app.MapDelete("/events/{id}", async (int id, EventDB db) =>
//{
//    if (await db.Events.FindAsync(id) is Event evnt)
//    {
//        db.Events.Remove(evnt);
//        await db.SaveChangesAsync();
//        return Results.Ok($"Event med id:{id} blev slettet");
//    }

//    return Results.NotFound();
//});

//app.MapGet("/users", async (EventDB db) =>
//    await db.Users.ToListAsync());

//app.MapGet("/users/{id}", async (int id, EventDB db) =>
//    await db.Users.FindAsync(id)
//        is User user
//            ? Results.Ok(user)
//            : Results.NotFound());

//app.MapPost("/users", async (User user, EventDB db, IValidator<User> validator) =>
//{
//    var validationResult = await validator.ValidateAsync(user);
//    if (!validationResult.IsValid)
//    {
//        return Results.BadRequest(validationResult.Errors);
//    }
//    db.Users.Add(user);
//    await db.SaveChangesAsync();

//    return Results.Created($"/users/{user.UserId}", user);
//});

//app.MapDelete("/users/{id}", async (int id, EventDB db) =>
//{
//    if (await db.Users.FindAsync(id) is User user)
//    {
//        db.Users.Remove(user);
//        await db.SaveChangesAsync();
//        return Results.NoContent();
//    }

//    return Results.NotFound();
//});

//app.MapGet("/bookings", async (EventDB db) =>
//    await db.Bookings.ToListAsync());

//app.MapGet("/bookings/{id}", async (int id, EventDB db) =>
//    await db.Bookings.FindAsync(id)
//        is Booking booking
//            ? Results.Ok(booking)
//            : Results.NotFound());

//app.MapPost("/bookings", async (Booking booking, EventDB db) =>
//{
//    db.Bookings.Add(booking);
//    await db.SaveChangesAsync();

//    return Results.Created($"/bookings/{booking.BookingId}", booking);
//});

//app.MapPut("/bookings/{id}", async (int id, Booking inputBooking, EventDB db) =>
//{
//    var booking = await db.Bookings.FindAsync(id);

//    if (booking is null) return Results.NotFound();

//    booking.BookingDate = inputBooking.BookingDate;
//    booking.EventId = inputBooking.EventId;
//    booking.UserId = inputBooking.UserId;

//    await db.SaveChangesAsync();

//    return Results.NoContent();
//});

//app.MapDelete("/bookings/{id}", async (int id, EventDB db) =>
//{
//    if (await db.Bookings.FindAsync(id) is Booking booking)
//    {
//        db.Bookings.Remove(booking);
//        await db.SaveChangesAsync();
//        return Results.NoContent();
//    }

//    return Results.NotFound();
//});


app.Run();
