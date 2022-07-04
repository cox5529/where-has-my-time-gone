<script setup lang="ts">
const props = defineProps<{
  sm?: boolean;
  color: string;
  type: 'submit' | 'button' | 'link';
  to?: string;
  disabled?: boolean;
}>();

const dynamicClass = {
  'rounded-lg shadow font-semibold text-center text-base inline-block text-white':
    true,
  'px-2 py-0.5': props.sm,
  'px-4 py-2': !props.sm,
  'cursor-not-allowed': props.disabled,
  'bg-blue-400': props.color === 'primary' && props.disabled,
  'bg-blue-500 hover:bg-blue-400 active:bg-blue-700':
    props.color === 'primary' && !props.disabled,
  'bg-gray-400': props.color === 'secondary' && props.disabled,
  'bg-gray-500 hover:bg-gray-400 active:bg-gray-700':
    props.color === 'secondary' && !props.disabled,
  'bg-red-400': props.color === 'danger' && props.disabled,
  'bg-red-500 hover:bg-red-400 active:bg-red-700':
    props.color === 'danger' && !props.disabled,
  'bg-green-400': props.color === 'success' && props.disabled,
  'bg-green-500 hover:bg-green-400 active:bg-green-700':
    props.color === 'success' && !props.disabled,
};
</script>

<template>
  <RouterLink v-if="props.type === 'link'" :to="props.to" :class="dynamicClass">
    <slot></slot>
  </RouterLink>
  <button v-else :class="dynamicClass" :type="props.type">
    <slot></slot>
  </button>
</template>
