import { Injectable } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import {
  Issuer,
  Strategy,
  TokenSet,
  UserinfoResponse,
  Client,
} from 'openid-client';
import { ConfigService } from '@nestjs/config';

@Injectable()
export class OidcStrategy extends PassportStrategy(Strategy, 'oidc') {
  constructor(private readonly configService: ConfigService) {
    // Read config synchronously (constructor is sync)
    const authority =
      configService.get<string>('IDP_AUTHORITY') ?? 'https://localhost:44392';
    const clientId =
      configService.get<string>('IDP_CLIENT_ID') ??
      'Orders.Microservice.BFF.ClientID';
    const clientSecret =
      configService.get<string>('IDP_CLIENT_SECRET') ?? 'change-me';
    const callbackUrl =
      configService.get<string>('IDP_CALLBACK_URL') ??
      'https://localhost:33800/api/auth/callback';
    const scopes =
      configService.get<string>('IDP_SCOPES') ??
      'openid profile email orders_api';

    // Build issuer metadata explicitly (Duende standard endpoints)
    const issuer = new Issuer({
      issuer: authority,
      authorization_endpoint: `${authority}/connect/authorize`,
      token_endpoint: `${authority}/connect/token`,
      userinfo_endpoint: `${authority}/connect/userinfo`,
      jwks_uri: `${authority}/.well-known/openid-configuration/jwks`,
    });

    // Create a proper openid-client Client instance
    const client: Client = new issuer.Client({
      client_id: clientId,
      client_secret: clientSecret,
      redirect_uris: [callbackUrl],
      response_types: ['code'],
    });

    // Call passport-openidconnect strategy from openid-client
    super({
      client, // ✅ REQUIRED: this must be an openid-client Client
      passReqToCallback: false,
      params: {
        scope: scopes,
      },
    });
  }

  /**
   * Called after openid-client has obtained & verified the token set.
   * The return value becomes `req.user`.
   */
  async validate(tokenset: TokenSet, userinfo: UserinfoResponse): Promise<any> {
    const user = {
      id: userinfo.sub,
      email: userinfo.email,
      name: userinfo.name,
      claims: userinfo,
      tokens: {
        accessToken: tokenset.access_token,
        idToken: tokenset.id_token,
        refreshToken: tokenset.refresh_token,
        expiresAt: tokenset.expires_at,
      },
    };

    return user;
  }
}