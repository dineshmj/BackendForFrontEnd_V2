import {
  CanActivate,
  ExecutionContext,
  Injectable,
  UnauthorizedException,
} from '@nestjs/common';
import { Request } from 'express';

@Injectable()
export class SessionAuthGuard implements CanActivate {
  canActivate(context: ExecutionContext): boolean {
    const req = context.switchToHttp().getRequest<Request>();
    const session: any = (req as any).session;

    // This is what we set in AuthController.callback
    const sessionUser = session?.bffUser;

    if (sessionUser) {
      // Hydrate req.user so controllers can use it
      (req as any).user = sessionUser;
      return true;
    }

    throw new UnauthorizedException('Not authenticated');
  }
}