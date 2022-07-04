<script setup lang="ts">
const props = defineProps<{ url?: string | null; name: string }>();
const classList = 'object-cover rounded-full';
</script>

<template>
  <img :class="classList" :src="props.url" v-if="props.url" :alt="props.name" />
  <font-awesome-icon :class="classList" v-else icon="fa-solid fa-circle-user" />
</template>
