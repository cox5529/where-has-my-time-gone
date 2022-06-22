<script setup lang="ts">
import { Field, useFieldError } from 'vee-validate';
import { computed } from 'vue';
import { ref } from 'vue';
import type * as Yup from 'yup';

import FormLabel from './FormLabel.vue';

const props = defineProps<{
  modelValue?: string;
  label?: string;
  name: string;
  type: string;
  disabled?: boolean;
  placeholder?: string;
  autocomplete?: boolean;
  min?: number;
  max?: number;
  step?: number;
  rules?: Yup.AnySchema;
}>();

const emits = defineEmits<{ (e: 'update:modelValue', value: string): void }>();

const touched = ref(false);
const formValue = ref('');

const errorMessage = useFieldError(props.name);
const hasErrors = computed(() => !!errorMessage.value);

const pristine = computed(() => !touched.value && !hasErrors.value);

const valid = computed(
  () => touched.value && !hasErrors.value && !props.disabled
);

const invalid = computed(
  () => touched.value && hasErrors.value && !props.disabled
);

function onUpdate(ev: Event): void {
  onTouch();
  const value = (ev.target as HTMLInputElement).value;
  emits('update:modelValue', value);
  formValue.value = value;
}

function onTouch(): void {
  touched.value = true;
}
</script>

<template>
  <div>
    <FormLabel v-if="label">{{ label }}</FormLabel>
    <Field
      as="input"
      class="w-full px-4 py-2 rounded transition-colors border border-gray-400 outline-none"
      :class="{
        'mb-6': pristine,
        'border-green-500 mb-6': valid,
        'bg-red-100 border-red-500 mb-0': invalid,
        'bg-gray-300': props.disabled,
      }"
      type="text"
      :disabled="props.disabled"
      :rules="props.rules"
      :name="props.name"
      :value="props.modelValue"
      @input="onUpdate"
      @blur="onTouch"
      :validate-on-blur="true"
      :validate-on-input="true"
    />
    <p class="text-red-500 text-sm mt-1" v-if="errorMessage">
      {{ errorMessage }}
    </p>
  </div>
</template>
