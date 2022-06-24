import { defineStore } from 'pinia';

import type { Person } from '../models/person';
import type { TimeUsage } from '../models/time-usage';
import { useAuthenticationStore } from './authentication';

export const usePersonStore = defineStore({
  id: 'people',
  state: () => ({
    people: [] as Person[],
  }),
  getters: {},
  actions: {
    async addPerson(name: string): Promise<void> {
      const authStore = useAuthenticationStore();

      const response = await authStore.post('/api/usage/person', { name });
      if (!response.ok) {
        throw new Error();
      }

      this.people.push({
        id: parseInt(await response.text()),
        name,
        usages: [],
      });
    },

    async addUsage(id: number, timeUsage: TimeUsage): Promise<void> {
      const person = this.people.find((x) => x.id === id);
      if (!person) {
        return;
      }

      const authStore = useAuthenticationStore();
      const response = await authStore.post('/api/usage/time-usage', {
        personId: person.id,
        start: timeUsage.start,
        end: timeUsage.end,
      });
      if (!response.ok) {
        throw new Error();
      }

      timeUsage.id = parseInt(await response.text());

      person.usages.push(timeUsage);
    },

    async load(): Promise<boolean> {
      const authStore = useAuthenticationStore();

      const response = await authStore.get('/api/usage/person', {});
      if (!response.ok) {
        return false;
      }

      const data: Person[] = await response.json();
      this.people = data;

      return true;
    },
  },
});
