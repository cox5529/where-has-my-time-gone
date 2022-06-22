import { getIdToken, type User } from '@firebase/auth';
import { defineStore } from 'pinia';

export interface AuthenticationState {
  user?: User | null;
}

export const useAuthenticationStore = defineStore({
  id: 'modals',
  state: (): AuthenticationState => ({
    user: undefined,
  }),
  getters: {
    async authToken(): Promise<string | null> {
      return this.user ? await getIdToken(this.user) : null;
    }
  },
  actions: {},
});
