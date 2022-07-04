<script setup lang="ts">
const props = defineProps<{
  to: string;
}>();
</script>

<template>
  <RouterLink :to="props.to" custom v-slot="{ href, navigate, isExactActive }">
    <a
      :href="href"
      @click="navigate"
      :class="{
        'text-white border-b-4 border-transparent px-4 py-1': true,
        'border-blue-500': isExactActive,
        'hover:border-blue-500': !isExactActive,
      }"
    >
      <slot></slot>
    </a>
  </RouterLink>
</template>
