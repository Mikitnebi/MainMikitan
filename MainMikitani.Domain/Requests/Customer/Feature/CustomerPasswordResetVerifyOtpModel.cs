namespace MainMikitan.Domain.Requests.Customer.Feature;

public abstract record CustomerPasswordResetVerifyOtpModel(string Otp, string Password);