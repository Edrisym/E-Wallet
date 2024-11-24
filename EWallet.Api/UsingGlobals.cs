global using System.Diagnostics.CodeAnalysis;
global using System.Text;

#region Microsoft

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

#region EWallet

global using EWallet.Api.Common.Extensions;
global using EWallet.Api.Common;
global using EWallet.Api.Common.Exceptions;
global using EWallet.Api.Common.Models;
global using DefaultColumnType = EWallet.Api.Common.StaticData.WalletDbContextDefaultColumnType;
global using SchemaTable = EWallet.Api.Common.StaticData.TableName;
global using ErrorMessages = EWallet.Api.Common.StaticData.ErrorMessages;
global using EWallet.Api.Currencies.EndPoints;
global using EWallet.Api.Wallets.EndPoints;

#endregion

#region DB

global using Microsoft.EntityFrameworkCore;

#endregion