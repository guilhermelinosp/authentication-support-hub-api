﻿namespace SupportHub.Auth.Domain.DTOs.Responses;

public record MessagesResponse
{
	public static string CODIGO_ENVIADO => "sending the code to your email or phone.";
	public static string CODIGO_ENVIADO_SIGN_UP => "sending the code to your email.";
	public static string CODIGO_CONFIRMADO => "confirmed successfully.";
	public static string SENHA_RESETADA => "password reset successfully.";
	
	public static string SIGN_OUT_CONFIRMADO => "Sessão encerrada com sucesso!.";
};