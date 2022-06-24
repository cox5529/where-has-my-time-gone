import { getIdToken, type User } from '@firebase/auth';
import { defineStore } from 'pinia';

type RequestMethod = 'post' | 'get' | 'put';

export interface AuthenticationState {
  user?: User | null;
}

export const useAuthenticationStore = defineStore({
  id: 'authentication',
  state: (): AuthenticationState => ({
    user: undefined,
  }),
  getters: {
    async authToken(): Promise<string | null> {
      return this.user ? await getIdToken(this.user) : null;
    },
  },
  actions: {
    async get(
      path: string,
      parameters: { [key: string]: string },
      init?: RequestInit
    ): Promise<Response> {
      const qs = Object.entries(parameters)
        .map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`)
        .join('&');
      path += `?${qs}`;
      return await this.request(path, 'get', init);
    },

    async post(path: string, body: any, init?: RequestInit): Promise<Response> {
      init ??= {};
      init.body = JSON.stringify(body);
      init.headers = { ...init.headers, 'content-type': 'application/json' };

      return await this.request(path, 'post', init);
    },

    async request(
      path: string,
      method: RequestMethod = 'post',
      init?: RequestInit
    ): Promise<Response> {
      init ??= {};
      init.headers = {
        ...init.headers,
        Authorization: `Bearer ${await this.authToken}`,
      };

      init.method = method;

      return await fetch(`${import.meta.env.VITE_BASE_API_URL}${path}`, init);
    },
  },
});
