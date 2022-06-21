<script setup lang="ts">
import { useModalStore, type ModalState } from '@/stores/modals';
import { computed } from 'vue';

const props = defineProps<{ name: keyof ModalState }>();

const modals = useModalStore();
const show = computed(() => modals[props.name]);

function hide(): void {
  modals.hide(props.name);
}
</script>

<template>
  <div
    class="fixed inset-0 bg-black/30 flex items-center justify-center p-4 animate-fadeIn"
    v-if="show"
    @click="hide"
  >
    <div
      @click.stop
      class="bg-white p-8 rounded-xl relative max-w-xl w-full animate-fromTop"
    >
      <button class="absolute top-2 right-2" @click="hide">
        <font-awesome-icon class="w-6 h-6" icon="fa-solid fa-circle-xmark" />
      </button>
      <slot></slot>
    </div>
  </div>
</template>
