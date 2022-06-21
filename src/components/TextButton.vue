<script setup lang="ts">
const props = defineProps<{
  sm?: boolean;
  color: string;
  type: 'submit' | 'button';
  disabled?: boolean;
}>();
</script>

<template>
  <button
    class="rounded-lg shadow font-semibold text-center text-base inline-block text-white"
    :class="{
      'px-2 py-0.5': sm,
      'px-4 py-2': !sm,
      'cursor-not-allowed': disabled,
      'bg-blue-400': color === 'primary' && disabled,
      'bg-blue-500 hover:bg-blue-400 active:bg-blue-700':
        color === 'primary' && !disabled,
      'bg-gray-400': color === 'secondary' && disabled,
      'bg-gray-500 hover:bg-gray-400 active:bg-gray-700':
        color === 'secondary' && !disabled,
      'bg-red-400': color === 'danger' && disabled,
      'bg-red-500 hover:bg-red-400 active:bg-red-700':
        color === 'danger' && !disabled,
      'bg-green-400': color === 'success' && disabled,
      'bg-green-500 hover:bg-green-400 active:bg-green-700':
        color === 'success' && !disabled,
    }"
    :type="props.type"
  >
    <slot></slot>
  </button>
</template>
