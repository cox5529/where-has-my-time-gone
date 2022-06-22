import { defineStore } from 'pinia';

export interface ModalState {
  addPerson: boolean;
}

export const useModalStore = defineStore({
  id: 'modals',
  state: (): ModalState => ({
    addPerson: false,
  }),
  getters: {},
  actions: {
    hide(modal: keyof ModalState) {
      this[modal] = false;
    },
  },
});
