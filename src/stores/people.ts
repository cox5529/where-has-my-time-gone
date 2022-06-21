import { defineStore } from 'pinia';

import type { Person } from '../models/person';
import type { TimeUsage } from '../models/time-usage';

export const usePersonStore = defineStore({
  id: 'people',
  state: () => ({
    people: [] as Person[],
  }),
  getters: {},
  actions: {
    addPerson(name: string) {
      this.people.push({
        id: this.people.length,
        name,
        usages: [],
      });
    },

    addUsage(id: number, timeUsage: TimeUsage) {
      const person = this.people.find((x) => x.id === id);
      if (!person) {
        return;
      }

      person.usages.push(timeUsage);
    },
  },
});
