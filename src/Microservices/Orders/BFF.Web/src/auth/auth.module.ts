import { Module } from '@nestjs/common';
import { PassportModule } from '@nestjs/passport';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { OidcStrategy } from './strategies/oidc.strategy';
import { SessionSerializer } from './session.serializer';
import { SilentAuthGuard } from './guards/silent-auth.guard';
import { SessionAuthGuard } from './guards/session-auth.guard';

@Module({
  imports: [PassportModule.register({ session: true })],
  controllers: [AuthController],
  providers: [
    AuthService,
    OidcStrategy,
    SessionSerializer,
    SilentAuthGuard,
    SessionAuthGuard,
  ],
  exports: [AuthService],
})
export class AuthModule {}