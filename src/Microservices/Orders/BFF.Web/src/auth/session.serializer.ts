import { Injectable } from '@nestjs/common';
import { PassportSerializer } from '@nestjs/passport';

@Injectable()
export class SessionSerializer extends PassportSerializer {
  serializeUser(user: any, done: (err: any, payload: any) => void) {
    const sessionUser = {
      id: user.id,
      email: user.email,
      name: user.name,
      claims: user.claims,
      tokens: user.tokens, // 👈 keep tokens in session
    };

    done(null, sessionUser);
  }

  deserializeUser(payload: any, done: (err: any, user: any) => void) {
    done(null, payload);
  }
}